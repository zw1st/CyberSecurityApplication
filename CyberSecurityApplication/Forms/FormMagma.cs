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

public partial class FormMagma : Form
{
    public FormMagma()
    {
        InitializeComponent();

        // Начальное состояние: пароль с клавиатуры
        UpdatePasswordModeUI();
    }

    #region Переключатели режима ввода пароля

    /// <summary>
    /// Обработчик переключателя "Пароль с клавиатуры".
    /// </summary>
    private void radioButtonPasswordFromKeyboard_CheckedChanged(object? sender, EventArgs e)
    {
        UpdatePasswordModeUI();
    }

    /// <summary>
    /// Обработчик переключателя "Пароль из файла".
    /// </summary>
    private void radioButtonPasswordFromFile_CheckedChanged(object? sender, EventArgs e)
    {
        UpdatePasswordModeUI();
    }

    /// <summary>
    /// Обновляет доступность элементов управления в зависимости
    /// от выбранного режима ввода пароля.
    /// </summary>
    private void UpdatePasswordModeUI()
    {
        bool isFromKeyboard = radioButtonPasswordFromKeyboard.Checked;

        // Поле ввода пароля с клавиатуры
        textBoxPassword.Enabled = isFromKeyboard;

        // Поле и кнопка выбора файла с паролем
        textBoxPasswordFilePath.Enabled = !isFromKeyboard;
        buttonSelectPasswordFile.Enabled = !isFromKeyboard;
    }

    #endregion

    #region Кнопки выбора файлов

    /// <summary>
    /// Выбор файла с паролем.
    /// </summary>
    private void buttonSelectPasswordFile_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Выберите файл с паролем",
            Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
            FilterIndex = 1
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxPasswordFilePath.Text = openFileDialog.FileName;
        }
    }

    /// <summary>
    /// Выбор входного файла для шифрования.
    /// </summary>
    private void buttonSelectEncryptInput_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Выберите файл для шифрования",
            Filter = "Все файлы (*.*)|*.*|Документы Word (*.doc, *.docx)|*.doc;*.docx|Таблицы Excel (*.xls, *.xlsx)|*.xls;*.xlsx|Изображения (*.png, *.jpg, *.jpeg, *.bmp)|*.png;*.jpg;*.jpeg;*.bmp|PDF-файлы (*.pdf)|*.pdf",
            FilterIndex = 1
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxEncryptInput.Text = openFileDialog.FileName;

            // Предлагаем выходной файл по умолчанию
            if (string.IsNullOrEmpty(textBoxEncryptOutput.Text))
            {
                string defaultOutput = openFileDialog.FileName + ".enc";
                textBoxEncryptOutput.Text = defaultOutput;
            }
        }
    }

    /// <summary>
    /// Выбор выходного файла для шифрования.
    /// </summary>
    private void buttonSelectEncryptOutput_Click(object? sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog
        {
            Title = "Выберите файл для сохранения зашифрованных данных",
            Filter = "Зашифрованные файлы (*.enc)|*.enc|Все файлы (*.*)|*.*",
            FilterIndex = 1,
            FileName = "encrypted.enc"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxEncryptOutput.Text = saveFileDialog.FileName;
        }
    }

    /// <summary>
    /// Выбор входного файла для расшифрования.
    /// </summary>
    private void buttonSelectDecryptInput_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Выберите зашифрованный файл",
            Filter = "Зашифрованные файлы (*.enc)|*.enc|Все файлы (*.*)|*.*",
            FilterIndex = 1
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxDecryptInput.Text = openFileDialog.FileName;

            // Предлагаем выходной файл по умолчанию
            if (string.IsNullOrEmpty(textBoxDecryptOutput.Text))
            {
                string fileName = openFileDialog.FileName;
                // Убираем расширение .enc, если оно есть
                string defaultOutput = fileName.EndsWith(".enc")
                    ? fileName.Substring(0, fileName.Length - 4)
                    : fileName + ".dec";
                textBoxDecryptOutput.Text = defaultOutput;
            }
        }
    }

    /// <summary>
    /// Выбор выходного файла для расшифрования.
    /// </summary>
    private void buttonSelectDecryptOutput_Click(object? sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog
        {
            Title = "Выберите файл для сохранения расшифрованных данных",
            Filter = "Все файлы (*.*)|*.*",
            FilterIndex = 1,
            FileName = "decrypted"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxDecryptOutput.Text = saveFileDialog.FileName;
        }
    }

    #endregion

    #region Кнопки запуска шифрования/расшифрования

    /// <summary>
    /// Запуск шифрования.
    /// </summary>
    private void buttonEncrypt_Click(object? sender, EventArgs e)
    {
        try
        {
            // Валидация входных данных
            if (!ValidateEncryptInputs(out string? errorMessage))
            {
                ShowError(errorMessage!);
                return;
            }

            // Получаем пароль
            string password = GetPassword();

            // Выполняем шифрование
            string inputPath = textBoxEncryptInput.Text.Trim();
            string outputPath = textBoxEncryptOutput.Text.Trim();

            Cursor = Cursors.WaitCursor;
            MagmaCipher.EncryptFile(inputPath, outputPath, password);
            Cursor = Cursors.Default;

            ShowSuccess($"Файл успешно зашифрован.\nРезультат: {outputPath}");
        }
        catch (Exception ex)
        {
            Cursor = Cursors.Default;
            ShowError($"Ошибка при шифровании: {ex.Message}");
        }
    }

    /// <summary>
    /// Запуск расшифрования.
    /// </summary>
    private void buttonDecrypt_Click(object? sender, EventArgs e)
    {
        try
        {
            // Валидация входных данных
            if (!ValidateDecryptInputs(out string? errorMessage))
            {
                ShowError(errorMessage!);
                return;
            }

            // Получаем пароль
            string password = GetPassword();

            // Выполняем расшифрование
            string inputPath = textBoxDecryptInput.Text.Trim();
            string outputPath = textBoxDecryptOutput.Text.Trim();

            Cursor = Cursors.WaitCursor;
            MagmaCipher.DecryptFile(inputPath, outputPath, password);
            Cursor = Cursors.Default;

            ShowSuccess($"Файл успешно расшифрован.\nРезультат: {outputPath}");
        }
        catch (Exception ex)
        {
            Cursor = Cursors.Default;
            ShowError($"Ошибка при расшифровании: {ex.Message}");
        }
    }

    #endregion

    #region Валидация

    /// <summary>
    /// Проверяет корректность входных данных для шифрования.
    /// </summary>
    private bool ValidateEncryptInputs(out string? errorMessage)
    {
        // Проверка входного файла
        if (string.IsNullOrWhiteSpace(textBoxEncryptInput.Text))
        {
            errorMessage = "Не указан входной файл для шифрования.";
            return false;
        }

        if (!File.Exists(textBoxEncryptInput.Text.Trim()))
        {
            errorMessage = $"Входной файл не найден: {textBoxEncryptInput.Text.Trim()}";
            return false;
        }

        // Проверка выходного файла
        if (string.IsNullOrWhiteSpace(textBoxEncryptOutput.Text))
        {
            errorMessage = "Не указан выходной файл для шифрования.";
            return false;
        }

        // Проверка пароля
        if (!ValidatePassword(out errorMessage))
        {
            return false;
        }

        errorMessage = null;
        return true;
    }

    /// <summary>
    /// Проверяет корректность входных данных для расшифрования.
    /// </summary>
    private bool ValidateDecryptInputs(out string? errorMessage)
    {
        // Проверка входного файла
        if (string.IsNullOrWhiteSpace(textBoxDecryptInput.Text))
        {
            errorMessage = "Не указан входной файл для расшифрования.";
            return false;
        }

        if (!File.Exists(textBoxDecryptInput.Text.Trim()))
        {
            errorMessage = $"Входной файл не найден: {textBoxDecryptInput.Text.Trim()}";
            return false;
        }

        // Проверка выходного файла
        if (string.IsNullOrWhiteSpace(textBoxDecryptOutput.Text))
        {
            errorMessage = "Не указан выходной файл для расшифрования.";
            return false;
        }

        // Проверка пароля
        if (!ValidatePassword(out errorMessage))
        {
            return false;
        }

        errorMessage = null;
        return true;
    }

    /// <summary>
    /// Проверяет корректность пароля в зависимости от выбранного режима.
    /// </summary>
    private bool ValidatePassword(out string? errorMessage)
    {
        if (radioButtonPasswordFromFile.Checked)
        {
            // Режим: пароль из файла
            if (string.IsNullOrWhiteSpace(textBoxPasswordFilePath.Text))
            {
                errorMessage = "Не указан файл с паролем.";
                return false;
            }

            if (!File.Exists(textBoxPasswordFilePath.Text.Trim()))
            {
                errorMessage = $"Файл с паролем не найден: {textBoxPasswordFilePath.Text.Trim()}";
                return false;
            }
        }
        // Режим: пароль с клавиатуры — пустой пароль разрешён, ничего не проверяем

        errorMessage = null;
        return true;
    }

    #endregion

    #region Вспомогательные методы

    /// <summary>
    /// Получает пароль в зависимости от выбранного режима.
    /// </summary>
    private string GetPassword()
    {
        if (radioButtonPasswordFromFile.Checked)
        {
            // Читаем пароль из файла
            string filePath = textBoxPasswordFilePath.Text.Trim();
            return File.ReadAllText(filePath).Trim();
        }
        else
        {
            // Пароль с клавиатуры (может быть пустым)
            return textBoxPassword.Text;
        }
    }

    /// <summary>
    /// Показывает сообщение об ошибке.
    /// </summary>
    private void ShowError(string message)
    {
        MessageBox.Show(message, "Ошибка",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>
    /// Показывает сообщение об успешном выполнении.
    /// </summary>
    private void ShowSuccess(string message)
    {
        MessageBox.Show(message, "Успех",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion
}