using CyberSecurityApplication.Db;

namespace CyberSecurityApplication.Crypto;

using System;
using System.Linq;

/// <summary>
/// Сервис для проверки паролей на соответствие правилам.
/// Реализует ограничение по варианту 14:
/// "Несовпадение с именем пользователя, записанным в обратном порядке."
/// </summary>
public static class PasswordPolicyService
{
    /// <summary>
    /// Проверяет пароль на соответствие всем правилам для указанного пользователя.
    /// </summary>
    /// <param name="username">Имя пользователя (для проверки ограничений).</param>
    /// <param name="password">Проверяемый пароль.</param>
    /// <param name="userSettings">Настройки пользователя из БД.</param>
    /// <returns>Результат валидации с сообщением об ошибке, если пароль не прошёл.</returns>
    public static PasswordValidationResult Validate(string username, string password, User userSettings)
    {
        if (userSettings is null)
            throw new ArgumentNullException(nameof(userSettings));

        // 1. Проверка минимальной длины пароля.
        //    Применяется всегда, если установлено значение > 0.
        if (password.Length < userSettings.MinPasswordLength)
        {
            return PasswordValidationResult.Fail(
                $"Пароль должен содержать не менее {userSettings.MinPasswordLength} символов.");
        }

        // 2. Проверка индивидуальных ограничений (если включены).
        if (userSettings.PasswordRestrictionsEnabled)
        {
            var restrictionError = CheckVariantRestriction(username, password);
            if (restrictionError is not null)
                return PasswordValidationResult.Fail(restrictionError);
        }

        return PasswordValidationResult.Success();
    }

    /// <summary>
    /// Проверка ограничения по варианту 14:
    /// Пароль не должен совпадать с именем пользователя, записанным в обратном порядке.
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="password">Проверяемый пароль.</param>
    /// <returns>Сообщение об ошибке, если ограничение нарушено; иначе null.</returns>
    private static string? CheckVariantRestriction(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        var reversedUsername = new string(username.Reverse().ToArray());

        if (string.Equals(password, reversedUsername, StringComparison.OrdinalIgnoreCase))
        {
            return "Пароль не должен совпадать с именем пользователя, записанным в обратном порядке.";
        }

        return null;
    }
}