using CyberSecurityApplication.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CyberSecurityApplication
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void buttonHash_Click(object sender, EventArgs e)
        {
            var hash = HashService.HashPassword(textBoxPassword.Text, textBoxSalt.Text);
            textBoxHash.Text = hash;
        }

        private void buttonMakeSalt_Click(object sender, EventArgs e)
        {
            textBoxMakeSalt.Text = HashService.GenerateSalt();
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            // Валидация входных данных
            if (string.IsNullOrEmpty(textBoxPhrase.Text))
            {
                MessageBox.Show("Введите парольную фразу", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBoxText.Text))
            {
                MessageBox.Show("Введите текст для шифрования", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Получаем парольную фразу и открытый текст
                string passphrase = textBoxPhrase.Text;
                string plainText = textBoxText.Text;

                // 2. Генерируем ключ DES из парольной фразы
                byte[] key = EncryptionService.DeriveKey(passphrase);

                // 3. Преобразуем текст в байты
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                // 4. Шифруем
                byte[] encryptedBytes = EncryptionService.Encrypt(plainBytes, key);

                // 5. Выводим результат в hex-виде
                textBoxKey.Text = BytesToHex(key);
                textBoxRes.Text = BytesToHex(encryptedBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка шифрования: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вспомогательный метод для отображения байтов в hex-виде.
        /// </summary>
        private string BytesToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRes.Text))
            {
                MessageBox.Show("Нет данных для расшифрования", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBoxKey.Text))
            {
                MessageBox.Show("Нет ключа для расшифрования", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                byte[] encryptedBytes = HexToBytes(textBoxRes.Text);
                byte[] key = HexToBytes(textBoxKey.Text);

                byte[] decryptedBytes;
                bool success = EncryptionService.TryDecrypt(encryptedBytes, key, out decryptedBytes);

                if (success)
                {
                    textBoxDecrypted.Text = Encoding.UTF8.GetString(decryptedBytes);
                    MessageBox.Show("Расшифрование успешно", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не удалось расшифровать. Неверный ключ или повреждённые данные.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат hex-строки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вспомогательный метод для преобразования hex-строки в байты.
        /// </summary>
        private byte[] HexToBytes(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                throw new FormatException("Пустая строка");
            if (hex.Length % 2 != 0)
                throw new FormatException("Нечётная длина hex-строки");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

    }
}
