using System.Text;
using System.Security.Cryptography;

namespace CyberSecurityApplication.Crypto;


/// <summary>
/// Сервис для шифрования/расшифрования данных алгоритмом DES в режиме CBC.
/// Также предоставляет метод деривации ключа из парольной фразы.
/// </summary>
public static class EncryptionService
{
    /// <summary>
    /// Генерирует 8-байтный ключ DES из парольной фразы.
    /// Алгоритм: MD5(UTF8(passphrase)), берём первые 8 байт.
    /// </summary>
    /// <param name="passphrase">Парольная фраза (может быть пустой при первом запуске).</param>
    /// <returns>8-байтный ключ для DES.</returns>
    public static byte[] DeriveKey(string passphrase)
    {
        passphrase ??= string.Empty;

        byte[] passphraseBytes = Encoding.UTF8.GetBytes(passphrase);
        using (var md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(passphraseBytes);
            byte[] key = new byte[8];
            Array.Copy(hash, key, 8);
            return key;
        }
    }

    /// <summary>
    /// Шифрует данные алгоритмом DES в режиме CBC.
    /// IV = 0 (согласно варианту задания: "Добавление к ключу случайного значения: Нет").
    /// Padding: PKCS7 (стандартный).
    /// </summary>
    /// <param name="data">Открытые данные (должны иметь длину > 0).</param>
    /// <param name="key">8-байтный ключ DES.</param>
    /// <returns>Зашифрованные данные.</returns>
    public static byte[] Encrypt(byte[] data, byte[] key)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        if (key == null || key.Length != 8)
            throw new ArgumentException("Key must be 8 bytes", nameof(key));

        using (var des = DES.Create())
        {
            des.Key = key;
            des.IV = new byte[8]; // IV = 0 согласно варианту задания
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;

            using (var encryptor = des.CreateEncryptor())
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                }
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Пытается расшифровать данные алгоритмом DES в режиме CBC.
    /// При неверном ключе (неверный padding или повреждённые данные) возвращает false.
    /// </summary>
    /// <param name="data">Зашифрованные данные.</param>
    /// <param name="key">8-байтный ключ DES.</param>
    /// <param name="result">Расшифрованные данные (если успешно).</param>
    /// <returns>True, если расшифровка успешна; иначе false.</returns>
    public static bool TryDecrypt(byte[] data, byte[] key, out byte[] result)
    {
        result = null;

        if (data == null || data.Length == 0)
            return false;
        if (key == null || key.Length != 8)
            return false;

        try
        {
            using (var des = DES.Create())
            {
                des.Key = key;
                des.IV = new byte[8]; // IV = 0 согласно варианту задания
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                using (var decryptor = des.CreateDecryptor())
                using (var ms = new MemoryStream(data))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var output = new MemoryStream())
                {
                    cs.CopyTo(output);
                    result = output.ToArray();
                    return true;
                }
            }
        }
        catch (CryptographicException)
        {
            // Неверный ключ → неверный padding → CryptographicException
            return false;
        }
        catch (Exception)
        {
            // Любая другая ошибка (повреждённые данные и т.п.)
            return false;
        }
    }
}