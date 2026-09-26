namespace CyberSecurityApplication.Db;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public bool IsLocked { get; set; }
    public int MinPasswordLength { get; set; }
    public int PasswordDurationMonths { get; set; }
    public bool PasswordRestrictionsEnabled { get; set; }
    public DateTime? LastPasswordChangeDate { get; set; }

    /// <summary>
    /// Определяет, является ли пользователь администратором.
    /// ADMIN определяется по имени, отдельного флага нет.
    /// </summary>
    public bool IsAdmin => string.Equals(Username, "ADMIN", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Проверяет, истёк ли срок действия пароля.
    /// </summary>
    public bool IsPasswordExpired()
    {
        if (PasswordDurationMonths == 0)
            return false; // Бессрочный пароль

        if (!LastPasswordChangeDate.HasValue)
            return true; // Пароль не установлен

        return LastPasswordChangeDate.Value.AddMonths(PasswordDurationMonths) < DateTime.Now;
    }

    /// <summary>
    /// Проверяет, установлен ли пароль.
    /// </summary>
    public bool HasPassword => !string.IsNullOrEmpty(PasswordHash);
}