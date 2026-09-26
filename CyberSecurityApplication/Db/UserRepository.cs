using CyberSecurityApplication.Crypto;

namespace CyberSecurityApplication.Db;

using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

/// <summary>
/// Репозиторий для работы с пользователями в БД.
/// Отвечает только за CRUD-операции. Не содержит бизнес-логики.
/// </summary>
public sealed class UserRepository
{
    private readonly DatabaseManager _dbManager;

    public UserRepository(DatabaseManager dbManager)
    {
        _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));
    }

    /// <summary>
    /// Активное подключение к БД. Получается через DatabaseManager.
    /// </summary>
    private SqliteConnection Connection => _dbManager.GetConnection();

    #region Чтение

    /// <summary>
    /// Находит пользователя по имени.
    /// </summary>
    /// <returns>User или null, если не найден.</returns>
    public User? GetUserByUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return null;

        using var command = Connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Username, PasswordHash, Salt, IsLocked, 
                   MinPasswordLength, PasswordDurationMonths, 
                   PasswordRestrictionsEnabled, LastPasswordChangeDate
            FROM Users
            WHERE Username = @username
            LIMIT 1";
        command.Parameters.AddWithValue("@username", username);

        // SingleRow — подсказка для SQLite, что ожидается одна строка
        using var reader = command.ExecuteReader(System.Data.CommandBehavior.SingleRow);
        return reader.Read() ? MapUser(reader) : null;
    }

    /// <summary>
    /// Получает список всех пользователей, отсортированных по имени.
    /// </summary>
    public List<User> GetAllUsers()
    {
        var users = new List<User>();

        using var command = Connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Username, PasswordHash, Salt, IsLocked, 
                   MinPasswordLength, PasswordDurationMonths, 
                   PasswordRestrictionsEnabled, LastPasswordChangeDate
            FROM Users
            ORDER BY Username";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(MapUser(reader));
        }

        return users;
    }

    /// <summary>
    /// Проверяет, существует ли пользователь с таким именем.
    /// </summary>
    public bool UsernameExists(string username)
    {
        using var command = Connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @username";
        command.Parameters.AddWithValue("@username", username);
        var count = (long)command.ExecuteScalar()!;
        return count > 0;
    }

    #endregion

    #region Создание

    /// <summary>
    /// Добавляет нового пользователя с пустым паролем.
    /// </summary>
    /// <returns>True, если пользователь создан; иначе (имя занято) — false.</returns>
    public bool AddUser(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        if (UsernameExists(username))
            return false;

        using var command = Connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Users (Username, PasswordHash, Salt, IsLocked, 
                              MinPasswordLength, PasswordDurationMonths, 
                              PasswordRestrictionsEnabled, LastPasswordChangeDate)
            VALUES (@username, '', '', 0, 0, 0, 0, NULL)";
        command.Parameters.AddWithValue("@username", username.Trim());

        var rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    #endregion

    #region Обновление

    /// <summary>
    /// Обновляет данные пользователя (кроме пароля).
    /// </summary>
    public bool UpdateUser(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        using var command = Connection.CreateCommand();
        command.CommandText = @"
            UPDATE Users
            SET IsLocked = @isLocked,
                MinPasswordLength = @minLength,
                PasswordDurationMonths = @duration,
                PasswordRestrictionsEnabled = @restrictions
            WHERE Id = @id";

        command.Parameters.AddWithValue("@id", user.Id);
        command.Parameters.AddWithValue("@isLocked", user.IsLocked ? 1 : 0);
        command.Parameters.AddWithValue("@minLength", user.MinPasswordLength);
        command.Parameters.AddWithValue("@duration", user.PasswordDurationMonths);
        command.Parameters.AddWithValue("@restrictions", user.PasswordRestrictionsEnabled ? 1 : 0);

        var rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    /// <summary>
    /// Меняет пароль пользователя. Генерирует новую соль.
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <param name="newPassword">Новый пароль (может быть пустым для сброса).</param>
    public bool ChangePassword(string username, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        // Генерируем новую соль для каждого изменения пароля
        var newSalt = HashService.GenerateSalt();
        var newHash = HashService.HashPassword(newPassword, newSalt);

        using var command = Connection.CreateCommand();
        command.CommandText = @"
            UPDATE Users
            SET PasswordHash = @hash,
                Salt = @salt,
                LastPasswordChangeDate = datetime('now')
            WHERE Username = @username";

        command.Parameters.AddWithValue("@hash", newHash);
        command.Parameters.AddWithValue("@salt", newSalt);
        command.Parameters.AddWithValue("@username", username);

        var rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    /// <summary>
    /// Блокирует или разблокирует пользователя.
    /// </summary>
    public bool SetLocked(string username, bool isLocked)
    {
        using var command = Connection.CreateCommand();
        command.CommandText = "UPDATE Users SET IsLocked = @locked WHERE Username = @username";
        command.Parameters.AddWithValue("@locked", isLocked ? 1 : 0);
        command.Parameters.AddWithValue("@username", username);

        var rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    #endregion

    #region Удаление

    /// <summary>
    /// Удаляет пользователя по имени. ADMIN удалить нельзя.
    /// </summary>
    public bool DeleteUser(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;

        if (string.Equals(username, "ADMIN", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot delete ADMIN user.");

        using var command = Connection.CreateCommand();
        command.CommandText = "DELETE FROM Users WHERE Username = @username";
        command.Parameters.AddWithValue("@username", username);

        var rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    #endregion

    #region Аутентификация

    /// <summary>
    /// Проверяет правильность пароля пользователя.
    /// Учитывает случай пустого пароля (при первом входе).
    /// </summary>
    public bool VerifyPassword(string username, string password)
    {
        var user = GetUserByUsername(username);
        if (user is null)
            return false;

        return HashService.VerifyPassword(password, user.Salt, user.PasswordHash);
    }

    #endregion

    #region Приватные методы

    /// <summary>
    /// Маппит текущую строку SqliteDataReader в объект User.
    /// </summary>
    private static User MapUser(SqliteDataReader reader)
    {
        return new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            PasswordHash = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
            Salt = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
            IsLocked = reader.GetInt32(4) == 1,
            MinPasswordLength = reader.GetInt32(5),
            PasswordDurationMonths = reader.GetInt32(6),
            PasswordRestrictionsEnabled = reader.GetInt32(7) == 1,
            LastPasswordChangeDate = TryParseDateTime(reader, 8)
        };
    }

    /// <summary>
    /// Безопасно парсит дату из SqliteDataReader.
    /// Возвращает null, если значение отсутствует или имеет неверный формат.
    /// </summary>
    private static DateTime? TryParseDateTime(SqliteDataReader reader, int ordinal)
    {
        if (reader.IsDBNull(ordinal))
            return null;

        var value = reader.GetString(ordinal);
        return DateTime.TryParse(value, out var date) ? date : null;
    }

    #endregion
}