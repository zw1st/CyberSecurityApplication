namespace CyberSecurityApplication.Crypto;

/// <summary>
/// Результат проверки пароля на соответствие правилам.
/// </summary>
public sealed class PasswordValidationResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    private PasswordValidationResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static PasswordValidationResult Success() => new(true, null);
    public static PasswordValidationResult Fail(string errorMessage) => new(false, errorMessage);
}