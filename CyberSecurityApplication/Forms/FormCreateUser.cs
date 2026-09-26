using CyberSecurityApplication.Crypto;
using CyberSecurityApplication.Db;

namespace CyberSecurityApplication.Forms;

using System;
using System.Windows.Forms;

public partial class FormCreateUser : Form
{
    private readonly UserRepository _userRepository;

    public FormCreateUser(UserRepository userRepository)
    {
        InitializeComponent();

        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        // Настройка значений по умолчанию для numericUpDown
        numericUpDownMinLength.Minimum = 0;
        numericUpDownMinLength.Maximum = 128;
        numericUpDownMinLength.Value = 0;

        numericUpDownDuration.Minimum = 0;
        numericUpDownDuration.Maximum = 120; // 10 лет максимум
        numericUpDownDuration.Value = 0;
    }

    private void buttonOk_Click(object? sender, EventArgs e)
    {
        var username = textBoxUsername.Text.Trim();
        var password = textBoxPassword.Text;

        // 1. Проверка имени пользователя
        if (string.IsNullOrWhiteSpace(username))
        {
            MessageBox.Show("Имя пользователя не может быть пустым.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxUsername.Focus();
            return;
        }

        // 2. Проверка уникальности имени
        if (_userRepository.UsernameExists(username))
        {
            MessageBox.Show($"Пользователь \"{username}\" уже существует.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxUsername.Focus();
            return;
        }

        // 3. Проверка имени на допустимые символы (опционально)
        if (username.Length > 64)
        {
            MessageBox.Show("Имя пользователя не должно превышать 64 символа.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxUsername.Focus();
            return;
        }

        // 4. Проверка пароля на соответствие ограничениям (если включены)
        if (!string.IsNullOrEmpty(password) && checkBoxPasswordRestrictions.Checked)
        {
            var tempUser = new User
            {
                Username = username,
                MinPasswordLength = (int)numericUpDownMinLength.Value,
                PasswordRestrictionsEnabled = true
            };

            var validationResult = PasswordPolicyService.Validate(username, password, tempUser);
            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage,
                    "Ошибка валидации пароля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.Clear();
                textBoxPassword.Focus();
                return;
            }
        }

        // 5. Проверка минимальной длины пароля
        if (password.Length < (int)numericUpDownMinLength.Value)
        {
            MessageBox.Show($"Пароль должен содержать не менее {numericUpDownMinLength.Value} символов.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxPassword.Focus();
            return;
        }

        // 6. Создание пользователя
        if (!_userRepository.AddUser(username))
        {
            MessageBox.Show("Не удалось создать пользователя.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // 7. Установка пароля (если указан)
        if (!string.IsNullOrEmpty(password))
        {
            _userRepository.ChangePassword(username, password);
        }

        // 8. Применение настроек
        var user = _userRepository.GetUserByUsername(username);
        if (user is not null)
        {
            user.MinPasswordLength = (int)numericUpDownMinLength.Value;
            user.PasswordDurationMonths = (int)numericUpDownDuration.Value;
            user.PasswordRestrictionsEnabled = checkBoxPasswordRestrictions.Checked;
            _userRepository.UpdateUser(user);
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
        numericUpDownDuration.Value = 0;
        numericUpDownMinLength.Value = 0;
        textBoxPassword.Text = string.Empty;
        textBoxUsername.Text = string.Empty;

    }
}