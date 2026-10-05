using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityApplication.Crypto;

using System;
using System.IO;
using System.Text;

/// <summary>
/// Собственная реализация алгоритма хеширования SHA-1.
/// Размер хэша: 160 бит (20 байт, 40 символов в hex).
/// </summary>
public static class Sha1Hasher
{
    /// <summary>
    /// Вычисляет SHA-1 хэш для массива байт.
    /// </summary>
    /// <param name="message">Входные данные произвольной длины.</param>
    /// <returns>Хэш в виде строки из 40 шестнадцатеричных символов.</returns>
    public static string HashBytes(byte[] message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Шаг 1: Дополнение сообщения (padding)
        byte[] paddedMessage = PadMessage(message);

        // Шаг 2: Инициализация регистров
        // Начальные значения являются константами стандарта
        uint h0 = 0x67452301;
        uint h1 = 0xEFCDAB89;
        uint h2 = 0x98BADCFE;
        uint h3 = 0x10325476;
        uint h4 = 0xC3D2E1F0;

        // Шаг 3: Обработка каждого блока по 512 бит (64 байта)
        for (int blockStart = 0; blockStart < paddedMessage.Length; blockStart += 64)
        {
            // 3.1. Разбиение блока на 16 слов по 32 бита (big-endian)
            uint[] w = new uint[80];
            for (int i = 0; i < 16; i++)
            {
                w[i] = ((uint)paddedMessage[blockStart + i * 4] << 24)
                     | ((uint)paddedMessage[blockStart + i * 4 + 1] << 16)
                     | ((uint)paddedMessage[blockStart + i * 4 + 2] << 8)
                     | ((uint)paddedMessage[blockStart + i * 4 + 3]);
            }

            // 3.2. Расширение слов с 16 до 80
            // Формула: W[i] = (W[i-3] XOR W[i-8] XOR W[i-14] XOR W[i-16]) <<< 1
            for (int i = 16; i < 80; i++)
            {
                uint temp = w[i - 3] ^ w[i - 8] ^ w[i - 14] ^ w[i - 16];
                w[i] = RotateLeft(temp, 1);
            }

            // 3.3. Копирование регистров во временные переменные
            uint a = h0;
            uint b = h1;
            uint c = h2;
            uint d = h3;
            uint e = h4;

            // 3.4. Выполнение 80 раундов
            for (int t = 0; t < 80; t++)
            {
                uint f, k;

                // Определение функции и константы в зависимости от номера раунда
                if (t < 20)
                {
                    // Раунды 0-19: функция выбора (мультиплексор)
                    f = (b & c) | (~b & d);
                    k = 0x5A827999;
                }
                else if (t < 40)
                {
                    // Раунды 20-39: функция чётности (XOR)
                    f = b ^ c ^ d;
                    k = 0x6ED9EBA1;
                }
                else if (t < 60)
                {
                    // Раунды 40-59: функция большинства
                    f = (b & c) | (b & d) | (c & d);
                    k = 0x8F1BBCDC;
                }
                else
                {
                    // Раунды 60-79: функция чётности (XOR)
                    f = b ^ c ^ d;
                    k = 0xCA62C1D6;
                }

                // Основная операция раунда
                // TEMP = (a <<< 5) + f(b,c,d) + e + K + W[t]
                uint temp = RotateLeft(a, 5) + f + e + k + w[t];

                // Сдвиг регистров
                e = d;
                d = c;
                c = RotateLeft(b, 30);
                b = a;
                a = temp;
            }

            // 3.5. Прибавление результатов к регистрам
            h0 += a;
            h1 += b;
            h2 += c;
            h3 += d;
            h4 += e;
        }

        // Шаг 4: Формирование итогового хэша (160 бит)
        // Конкатенация регистров в порядке H0 || H1 || H2 || H3 || H4
        byte[] hash = new byte[20];
        WriteUIntToBytes(h0, hash, 0);
        WriteUIntToBytes(h1, hash, 4);
        WriteUIntToBytes(h2, hash, 8);
        WriteUIntToBytes(h3, hash, 12);
        WriteUIntToBytes(h4, hash, 16);

        // Преобразование в шестнадцатеричную строку
        return BytesToHex(hash);
    }

    /// <summary>
    /// Вычисляет SHA-1 хэш для файла.
    /// </summary>
    /// <param name="filePath">Путь к файлу.</param>
    /// <returns>Хэш в виде строки из 40 шестнадцатеричных символов.</returns>
    public static string HashFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл не найден", filePath);

        byte[] fileBytes = File.ReadAllBytes(filePath);
        return HashBytes(fileBytes);
    }

    /// <summary>
    /// Вычисляет SHA-1 хэш для строки (удобно для тестирования).
    /// </summary>
    /// <param name="text">Входная строка.</param>
    /// <returns>Хэш в виде строки из 40 шестнадцатеричных символов.</returns>
    public static string HashString(string text)
    {
        byte[] messageBytes = Encoding.UTF8.GetBytes(text);
        return HashBytes(messageBytes);
    }   

    #region Вспомогательные методы

    /// <summary>
    /// Дополняет сообщение согласно стандарту SHA-1:
    /// 1. Добавляется бит 1 (байт 0x80)
    /// 2. Добавляются нулевые биты до длины ≡ 448 (mod 512)
    /// 3. Добавляется 64-битная длина сообщения в битах (big-endian)
    /// </summary>
    private static byte[] PadMessage(byte[] message)
    {
        long messageLengthBits = (long)message.Length * 8;

        // Используем MemoryStream для эффективной работы с большими данными
        using (var ms = new MemoryStream())
        {
            // Записываем исходное сообщение
            ms.Write(message, 0, message.Length);

            // Добавляем бит 1 (байт 0x80)
            ms.WriteByte(0x80);

            // Добавляем нулевые байты, пока длина не станет ≡ 56 (mod 64)
            // 56 байт = 448 бит
            while (ms.Length % 64 != 56)
            {
                ms.WriteByte(0x00);
            }

            // Добавляем 64-битную длину сообщения в битах (big-endian)
            // Старший байт записывается первым
            for (int i = 7; i >= 0; i--)
            {
                ms.WriteByte((byte)((messageLengthBits >> (i * 8)) & 0xFF));
            }

            return ms.ToArray();
        }
    }

    /// <summary>
    /// Циклический сдвиг 32-битного значения влево.
    /// </summary>
    private static uint RotateLeft(uint value, int shift)
    {
        return (value << shift) | (value >> (32 - shift));
    }

    /// <summary>
    /// Записывает 32-битное значение в массив байт в формате big-endian.
    /// </summary>
    private static void WriteUIntToBytes(uint value, byte[] buffer, int offset)
    {
        buffer[offset] = (byte)((value >> 24) & 0xFF);
        buffer[offset + 1] = (byte)((value >> 16) & 0xFF);
        buffer[offset + 2] = (byte)((value >> 8) & 0xFF);
        buffer[offset + 3] = (byte)(value & 0xFF);
    }

    /// <summary>
    /// Преобразует массив байт в шестнадцатеричную строку.
    /// </summary>
    private static string BytesToHex(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    #endregion
}