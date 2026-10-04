using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberSecurityApplication.Forms;

using CyberSecurityApplication.Crypto;
using System;
using System.IO;
using System.Windows.Forms;

public partial class HashFile : Form
{
    public HashFile()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обработчик кнопки выбора файла.
    /// Открывает диалог выбора файла и передаёт его в метод хеширования.
    /// </summary>
    private void buttonSelectFile_Click(object? sender, EventArgs e)
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Title = "Выберите файл для хеширования";
            openFileDialog.Filter = "Все файлы (*.*)|*.*|Текстовые файлы (*.txt)|*.txt|Документы Word (*.doc, *.docx)|*.doc;*.docx|Таблицы Excel (*.xls, *.xlsx)|*.xls;*.xlsx|Изображения (*.png, *.jpg, *.jpeg, *.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            openFileDialog.FilterIndex = 1; // По умолчанию "Все файлы"

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return; // Пользователь отменил выбор

            string filePath = openFileDialog.FileName;

            try
            {
                // Проверка размера файла (требование: не менее 1 кБ)
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length < 1024)
                {
                    MessageBox.Show(
                        $"Размер файла ({fileInfo.Length} байт) меньше минимально допустимого (1024 байт).\n" +
                        "Выберите файл размером не менее 1 кБ.",
                        "Недостаточный размер файла",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Отображаем путь к файлу и его размер
                textBoxFilePath.Text = filePath;
                //textBoxFileSize.Text = FormatFileSize(fileInfo.Length);

                // Вычисляем хэш
                string hash = Sha1Hasher.HashFile(filePath);

                // Отображаем результат
                textBoxHashResult.Text = hash;
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Файл слишком большой для обработки.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка чтения файла: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Форматирует размер файла в удобочитаемый вид.
    /// </summary>
    private static string FormatFileSize(long sizeInBytes)
    {
        if (sizeInBytes < 1024)
            return $"{sizeInBytes} Б";

        if (sizeInBytes < 1024 * 1024)
            return $"{sizeInBytes / 1024.0:F2} КБ";

        return $"{sizeInBytes / (1024.0 * 1024.0):F2} МБ";
    }
}