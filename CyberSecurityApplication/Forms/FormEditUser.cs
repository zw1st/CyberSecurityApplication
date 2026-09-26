using CyberSecurityApplication.Db;

namespace CyberSecurityApplication.Forms;

using System;
using System.Windows.Forms;

public partial class FormEditUser : Form
{
    private readonly UserRepository _userRepository;
    private readonly User _user;

    public FormEditUser(UserRepository userRepository, User user)
    {
        InitializeComponent();
        
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _user = user ?? throw new ArgumentNullException(nameof(user));

        // Заполняем поля текущими значениями
        textBoxUsername.Text = _user.Username;
        textBoxUsername.ReadOnly = true; // Имя менять нельзя
        
        numericUpDownMinLength.Minimum = 0;
        numericUpDownMinLength.Maximum = 128;
        numericUpDownMinLength.Value = _user.MinPasswordLength;

        numericUpDownDuration.Minimum = 0;
        numericUpDownDuration.Maximum = 120;
        numericUpDownDuration.Value = _user.PasswordDurationMonths;

        checkBoxRestrictions.Checked = _user.PasswordRestrictionsEnabled;
    }

    private void buttonOk_Click(object? sender, EventArgs e)
    {
        // Обновляем настройки пользователя
        _user.MinPasswordLength = (int)numericUpDownMinLength.Value;
        _user.PasswordDurationMonths = (int)numericUpDownDuration.Value;
        _user.PasswordRestrictionsEnabled = checkBoxRestrictions.Checked;

        if (_userRepository.UpdateUser(_user))
        {
            MessageBox.Show("Настройки пользователя обновлены.",
                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            MessageBox.Show("Не удалось обновить настройки.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}