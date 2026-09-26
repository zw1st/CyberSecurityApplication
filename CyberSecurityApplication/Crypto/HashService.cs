using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace CyberSecurityApplication.Crypto;


/// <summary>
/// Сервис для хеширования паролей с использованием MD5 и соли.
/// Соль хранится в БД в виде hex-строки (32 символа = 16 байт).
/// Хэш пароля также хранится в виде hex-строки (32 символа = 16 байт MD5).
/// </summary>
public static class HashService
{
    private const int SaltLengthBytes = 16;

    /// <summary>
    /// Генерирует криптографически стойкую соль размером 16 байт.
    /// </summary>
    /// <returns>Соль в виде hex-строки (32 символа).</returns>
    public static string GenerateSalt()
    {
        byte[] saltBytes = new byte[SaltLengthBytes];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return BytesToHex(saltBytes);
    }

    /// <summary>
    /// Вычисляет MD5-хэш пароля с учётом соли.
    /// Формула: MD5(UTF8(password) + salt_bytes)
    /// </summary>
    /// <param name="password">Пароль в открытом виде.</param>
    /// <param name="salt">Соль в виде hex-строки (получена из GenerateSalt).</param>
    /// <returns>Хэш в виде hex-строки (32 символа).</returns>
    public static string HashPassword(string password, string salt)
    {
        if (password == null) password = string.Empty;
        if (string.IsNullOrEmpty(salt))
            throw new ArgumentException("Salt cannot be null or empty", nameof(salt));

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = HexToBytes(salt);

        // Конкатенация: password_bytes + salt_bytes
        byte[] combined = new byte[passwordBytes.Length + saltBytes.Length];
        Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, combined, passwordBytes.Length, saltBytes.Length);

        using (var md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(combined);
            return BytesToHex(hashBytes);
        }
    }

    /// <summary>
    /// Проверяет соответствие пароля сохранённому хэшу.
    /// Использует time-safe сравнение для защиты от timing attacks.
    /// </summary>
    public static bool VerifyPassword(string password, string salt, string expectedHash)
    {
        if (string.IsNullOrEmpty(expectedHash)) return string.IsNullOrEmpty(password); ;

        string actualHash = HashPassword(password, salt);
        return TimeSafeCompare(actualHash, expectedHash);
    }

    /// <summary>
    /// Константное по времени сравнение строк.
    /// Защищает от timing attacks, когда злоумышленник может определить
    /// количество совпадающих символов по времени ответа.
    /// </summary>
    private static bool TimeSafeCompare(string a, string b)
    {
        if (a == null || b == null || a.Length != b.Length)
            return false;

        int diff = 0;
        for (int i = 0; i < a.Length; i++)
        {
            diff |= a[i] ^ b[i];
        }
        return diff == 0;
    }

    private static string BytesToHex(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    private static byte[] HexToBytes(string hex)
    {
        if (string.IsNullOrEmpty(hex))
            throw new ArgumentException("Hex string cannot be null or empty");
        if (hex.Length % 2 != 0)
            throw new ArgumentException("Invalid hex string length");

        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return bytes;
    }
}