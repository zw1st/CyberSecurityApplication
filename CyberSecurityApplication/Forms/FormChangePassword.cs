using CyberSecurityApplication.Crypto;
using CyberSecurityApplication.Db;

namespace CyberSecurityApplication.Forms;

using System;
using System.Windows.Forms;

public partial class FormChangePassword : Form
{
    private readonly UserRepository _userRepository;
    private readonly User _currentUser;

    public FormChangePassword(UserRepository userRepository, User currentUser)
    {
        InitializeComponent();
        
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
        var oldPassword = textBoxOldPassword.Text;
        var newPassword = textBoxNewPassword.Text;
        var newPasswordConfirmation = textBoxNewPasswordConfirmation.Text;

        // 1. Проверка: новый пароль и подтверждение должны быть заполнены
        if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newPasswordConfirmation))
        {
            MessageBox.Show("Новый пароль и подтверждение должны быть заполнены.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxNewPassword.Focus();
            return;
        }

        // 2. Если у пользователя уже есть пароль, старый пароль обязателен
        if (_currentUser.HasPassword && string.IsNullOrEmpty(oldPassword))
        {
            MessageBox.Show("Введите старый пароль.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxOldPassword.Focus();
            return;
        }

        // 3. Проверка старого пароля
        if (!_userRepository.VerifyPassword(_currentUser.Username, oldPassword))
        {
            MessageBox.Show("Неверный старый пароль.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxOldPassword.Clear();
            textBoxOldPassword.Focus();
            return;
        }

        // 4. Проверка, что новый пароль отличается от старого
        if (oldPassword == newPassword)
        {
            MessageBox.Show("Новый пароль должен отличаться от старого.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxNewPassword.Clear();
            textBoxNewPasswordConfirmation.Clear();
            textBoxNewPassword.Focus();
            return;
        }

        // 5. Проверка, что новый пароль и подтверждение совпадают
        if (newPassword != newPasswordConfirmation)
        {
            MessageBox.Show("Новый пароль и подтверждение не совпадают.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxNewPassword.Clear();
            textBoxNewPasswordConfirmation.Clear();
            textBoxNewPassword.Focus();
            return;
        }

        // 6. Проверка нового пароля на соответствие политике
        var validationResult = PasswordPolicyService.Validate(
            _currentUser.Username, newPassword, _currentUser);

        if (!validationResult.IsValid)
        {
            MessageBox.Show(validationResult.ErrorMessage,
                "Ошибка валидации пароля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxNewPassword.Clear();
            textBoxNewPasswordConfirmation.Clear();
            textBoxNewPassword.Focus();
            return;
        }

        // 7. Обновление пароля в БД
        if (_userRepository.ChangePassword(_currentUser.Username, newPassword))
        {
            MessageBox.Show("Пароль успешно изменён.",
                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            MessageBox.Show("Не удалось изменить пароль.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
        textBoxOldPassword.Text = "";
        textBoxNewPassword.Text = "";
        textBoxNewPasswordConfirmation.Text = "";
    }
}