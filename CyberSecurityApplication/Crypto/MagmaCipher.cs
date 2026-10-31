using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityApplication.Crypto;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Реализация алгоритма шифрования Магма (ГОСТ Р 34.12-2018, ранее ГОСТ 28147-89).
/// Режим: простая замена (аналогично ECB).
/// Размер блока: 64 бита (8 байт).
/// Размер ключа: 256 бит (32 байта).
/// </summary>
public static class MagmaCipher
{
    // Размер блока в байтах (64 бита = 8 байт)
    private const int BlockSizeBytes = 8;

    // Количество раундов
    private const int Rounds = 32;

    // Таблицы подстановки (S-блоки) из ГОСТ 28147-89
    // Каждая таблица: 4 бита входа -> 4 бита выхода
    private static readonly int[][] SBoxes =
    {
        new int[] { 4, 10, 9, 2, 13, 8, 0, 14, 6, 11, 1, 12, 7, 15, 5, 3 },
        new int[] { 14, 11, 4, 12, 6, 13, 15, 10, 2, 3, 8, 1, 0, 7, 5, 9 },
        new int[] { 5, 8, 1, 13, 10, 3, 4, 2, 14, 15, 12, 7, 6, 0, 9, 11 },
        new int[] { 7, 13, 10, 1, 0, 8, 9, 15, 14, 4, 6, 12, 11, 2, 5, 3 },
        new int[] { 6, 12, 7, 1, 5, 15, 13, 8, 4, 10, 9, 14, 0, 3, 11, 2 },
        new int[] { 4, 11, 10, 0, 7, 2, 1, 13, 3, 6, 8, 5, 9, 12, 15, 14 },
        new int[] { 13, 11, 4, 1, 3, 15, 5, 9, 0, 10, 14, 7, 6, 8, 2, 12 },
        new int[] { 1, 15, 13, 0, 5, 7, 10, 4, 9, 2, 3, 14, 6, 11, 8, 12 }
    };

    #region Публичные методы

    /// <summary>
    /// Шифрует файл в режиме простой замены.
    /// </summary>
    /// <param name="inputPath">Путь к исходному файлу.</param>
    /// <param name="outputPath">Путь к зашифрованному файлу.</param>
    /// <param name="password">Пароль для шифрования.</param>
    public static void EncryptFile(string inputPath, string outputPath, string password)
    {
        if (string.IsNullOrEmpty(inputPath))
            throw new ArgumentException("Не указан входной файл", nameof(inputPath));
        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Не указан выходной файл", nameof(outputPath));
        if (!File.Exists(inputPath))
            throw new FileNotFoundException("Входной файл не найден", inputPath);

        // Читаем файл
        byte[] data = File.ReadAllBytes(inputPath);

        // Получаем ключ из пароля
        byte[] key = DeriveKeyFromPassword(password);

        // Шифруем
        byte[] encrypted = EncryptBytes(data, key);

        // Записываем результат
        File.WriteAllBytes(outputPath, encrypted);
    }

    /// <summary>
    /// Расшифровывает файл в режиме простой замены.
    /// </summary>
    /// <param name="inputPath">Путь к зашифрованному файлу.</param>
    /// <param name="outputPath">Путь к расшифрованному файлу.</param>
    /// <param name="password">Пароль для расшифрования.</param>
    public static void DecryptFile(string inputPath, string outputPath, string password)
    {
        if (string.IsNullOrEmpty(inputPath))
            throw new ArgumentException("Не указан входной файл", nameof(inputPath));
        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Не указан выходной файл", nameof(outputPath));
        if (!File.Exists(inputPath))
            throw new FileNotFoundException("Входной файл не найден", inputPath);

        // Читаем файл
        byte[] data = File.ReadAllBytes(inputPath);

        // Получаем ключ из пароля
        byte[] key = DeriveKeyFromPassword(password);

        // Расшифровываем
        byte[] decrypted = DecryptBytes(data, key);

        // Записываем результат
        File.WriteAllBytes(outputPath, decrypted);
    }

    /// <summary>
    /// Шифрует массив байт в режиме простой замены.
    /// </summary>
    /// <param name="data">Открытые данные.</param>
    /// <param name="key">Ключ (32 байта = 256 бит).</param>
    /// <returns>Зашифрованные данные.</returns>
    public static byte[] EncryptBytes(byte[] data, byte[] key)
    {
        if (key == null || key.Length != 32)
            throw new ArgumentException("Ключ должен быть 32 байта (256 бит)", nameof(key));

        // Подготавливаем подключи
        uint[] subkeys = PrepareKey(key);
        uint[] keySchedule = GetEncryptionKeySchedule(subkeys);

        // Дополняем данные
        byte[] paddedData = Pad(data);

        // Шифруем каждый блок по 8 байт
        byte[] result = new byte[paddedData.Length];
        for (int i = 0; i < paddedData.Length; i += BlockSizeBytes)
        {
            // Читаем блок (64 бита = 2 слова по 32 бита) в little-endian
            uint left = BytesToUInt32(paddedData, i);
            uint right = BytesToUInt32(paddedData, i + 4);

            // Шифруем блок
            uint[] block = { left, right };
            uint[] encryptedBlock = new uint[2];
            ProcessBlock(block, keySchedule, encryptedBlock);

            // Записываем результат
            UInt32ToBytes(encryptedBlock[0], result, i);
            UInt32ToBytes(encryptedBlock[1], result, i + 4);
        }

        return result;
    }

    /// <summary>
    /// Расшифровывает массив байт в режиме простой замены.
    /// </summary>
    /// <param name="data">Зашифрованные данные.</param>
    /// <param name="key">Ключ (32 байта = 256 бит).</param>
    /// <returns>Расшифрованные данные.</returns>
    public static byte[] DecryptBytes(byte[] data, byte[] key)
    {
        if (key == null || key.Length != 32)
            throw new ArgumentException("Ключ должен быть 32 байта (256 бит)", nameof(key));
        if (data.Length % BlockSizeBytes != 0)
            throw new ArgumentException("Длина зашифрованных данных должна быть кратна 8 байтам", nameof(data));

        // Подготавливаем подключи
        uint[] subkeys = PrepareKey(key);
        uint[] keySchedule = GetDecryptionKeySchedule(subkeys);

        // Расшифровываем каждый блок по 8 байт
        byte[] result = new byte[data.Length];
        for (int i = 0; i < data.Length; i += BlockSizeBytes)
        {
            // Читаем блок
            uint left = BytesToUInt32(data, i);
            uint right = BytesToUInt32(data, i + 4);

            // Расшифровываем блок
            uint[] block = { left, right };
            uint[] decryptedBlock = new uint[2];
            ProcessBlock(block, keySchedule, decryptedBlock);

            // Записываем результат
            UInt32ToBytes(decryptedBlock[0], result, i);
            UInt32ToBytes(decryptedBlock[1], result, i + 4);
        }

        // Удаляем дополнение
        return Unpad(result);
    }

    /// <summary>
    /// Получает 256-битный ключ из пароля через SHA-256.
    /// </summary>
    /// <param name="password">Пароль (может быть пустым).</param>
    /// <returns>Ключ (32 байта = 256 бит).</returns>
    public static byte[] DeriveKeyFromPassword(string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password ?? string.Empty);

        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(passwordBytes);
        }
    }

    /// <summary>
    /// Получает 256-битный ключ из файла с паролем через SHA-256.
    /// </summary>
    /// <param name="filePath">Путь к файлу с паролем.</param>
    /// <returns>Ключ (32 байта = 256 бит).</returns>
    public static byte[] DeriveKeyFromPasswordFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("Не указан файл с паролем", nameof(filePath));
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл с паролем не найден", filePath);

        // Читаем пароль из файла (пустой файл -> пустая строка)
        string password = File.ReadAllText(filePath).Trim();

        return DeriveKeyFromPassword(password);
    }

    #endregion

    #region Внутренняя реализация алгоритма

    /// <summary>
    /// Подготавливает 8 подключей по 32 бита из 256-битного ключа.
    /// Порядок байт: little-endian.
    /// </summary>
    private static uint[] PrepareKey(byte[] key)
    {
        uint[] subkeys = new uint[8];
        for (int i = 0; i < 8; i++)
        {
            subkeys[i] = BytesToUInt32(key, i * 4);
        }
        return subkeys;
    }

    /// <summary>
    /// Формирует расписание ключей для шифрования.
    /// Раунды 1-8:   K1, K2, K3, K4, K5, K6, K7, K8
    /// Раунды 9-16:  K1, K2, K3, K4, K5, K6, K7, K8
    /// Раунды 17-24: K1, K2, K3, K4, K5, K6, K7, K8
    /// Раунды 25-32: K8, K7, K6, K5, K4, K3, K2, K1
    /// </summary>
    private static uint[] GetEncryptionKeySchedule(uint[] subkeys)
    {
        uint[] schedule = new uint[Rounds];

        // Раунды 1-24: K1..K8 три раза
        for (int i = 0; i < 24; i++)
        {
            schedule[i] = subkeys[i % 8];
        }

        // Раунды 25-32: K8..K1 (обратный порядок)
        for (int i = 0; i < 8; i++)
        {
            schedule[24 + i] = subkeys[7 - i];
        }

        return schedule;
    }

    /// <summary>
    /// Формирует расписание ключей для расшифрования.
    /// Это обратный порядок расписания шифрования.
    /// </summary>
    private static uint[] GetDecryptionKeySchedule(uint[] subkeys)
    {
        uint[] encryptionSchedule = GetEncryptionKeySchedule(subkeys);
        uint[] decryptionSchedule = new uint[Rounds];

        for (int i = 0; i < Rounds; i++)
        {
            decryptionSchedule[i] = encryptionSchedule[Rounds - 1 - i];
        }

        return decryptionSchedule;
    }

    /// <summary>
    /// Функция раунда Магмы: f(R, K).
    /// Шаг 1: Сложение по модулю 2^32
    /// Шаг 2: Подстановка через S-блоки
    /// Шаг 3: Циклический сдвиг влево на 11 бит
    /// </summary>
    private static uint RoundFunction(uint r, uint k)
    {
        // Шаг 1: Сложение по модулю 2^32
        uint sum = unchecked(r + k);

        // Шаг 2: Подстановка через S-блоки
        // Разбиваем 32-битное значение на 8 групп по 4 бита
        uint substituted = 0;
        for (int i = 0; i < 8; i++)
        {
            int nibble = (int)((sum >> (i * 4)) & 0x0F);
            int substitutedNibble = SBoxes[i][nibble];
            substituted |= (uint)substitutedNibble << (i * 4);
        }

        // Шаг 3: Циклический сдвиг влево на 11 бит
        return RotateLeft(substituted, 11);
    }

    /// <summary>
    /// Обрабатывает один блок (32 раунда сети Фейстеля).
    /// </summary>
    private static void ProcessBlock(uint[] block, uint[] keySchedule, uint[] output)
    {
        uint left = block[0];
        uint right = block[1];

        // 32 раунда сети Фейстеля
        for (int i = 0; i < Rounds; i++)
        {
            uint temp = left ^ RoundFunction(right, keySchedule[i]);
            left = right;
            right = temp;
        }

        // Обмен половин после последнего раунда
        output[0] = right;
        output[1] = left;
    }

    #endregion

    #region Дополнение (padding)

    /// <summary>
    /// Дополняет данные по схеме, аналогичной PKCS7.
    /// Каждый добавленный байт содержит количество добавленных байт.
    /// </summary>
    private static byte[] Pad(byte[] data)
    {
        int paddingLength = BlockSizeBytes - (data.Length % BlockSizeBytes);

        byte[] padded = new byte[data.Length + paddingLength];
        Array.Copy(data, padded, data.Length);

        // Каждый добавленный байт содержит количество добавленных байт
        for (int i = data.Length; i < padded.Length; i++)
        {
            padded[i] = (byte)paddingLength;
        }

        return padded;
    }

    /// <summary>
    /// Удаляет дополнение по схеме, аналогичной PKCS7.
    /// </summary>
    private static byte[] Unpad(byte[] data)
    {
        if (data.Length == 0 || data.Length % BlockSizeBytes != 0)
            throw new ArgumentException("Некорректные данные для удаления дополнения");

        int paddingLength = data[data.Length - 1];

        if (paddingLength < 1 || paddingLength > BlockSizeBytes)
            throw new ArgumentException("Некорректное дополнение (возможно, неверный пароль)");

        // Проверяем, что все байты дополнения корректны
        for (int i = data.Length - paddingLength; i < data.Length; i++)
        {
            if (data[i] != paddingLength)
                throw new ArgumentException("Некорректное дополнение (возможно, неверный пароль)");
        }

        byte[] unpadded = new byte[data.Length - paddingLength];
        Array.Copy(data, unpadded, unpadded.Length);

        return unpadded;
    }

    #endregion

    #region Вспомогательные методы

    /// <summary>
    /// Циклический сдвиг влево для 32-битного значения.
    /// </summary>
    private static uint RotateLeft(uint value, int shift)
    {
        return (value << shift) | (value >> (32 - shift));
    }

    /// <summary>
    /// Преобразует 4 байта в 32-битное значение (little-endian).
    /// </summary>
    private static uint BytesToUInt32(byte[] bytes, int offset)
    {
        return (uint)bytes[offset]
             | ((uint)bytes[offset + 1] << 8)
             | ((uint)bytes[offset + 2] << 16)
             | ((uint)bytes[offset + 3] << 24);
    }

    /// <summary>
    /// Преобразует 32-битное значение в 4 байта (little-endian).
    /// </summary>
    private static void UInt32ToBytes(uint value, byte[] bytes, int offset)
    {
        bytes[offset] = (byte)(value & 0xFF);
        bytes[offset + 1] = (byte)((value >> 8) & 0xFF);
        bytes[offset + 2] = (byte)((value >> 16) & 0xFF);
        bytes[offset + 3] = (byte)((value >> 24) & 0xFF);
    }

    #endregion
}
