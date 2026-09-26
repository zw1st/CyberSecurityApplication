using CyberSecurityApplication.Db;

namespace CyberSecurityApplication.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public partial class FormManageUsers : Form
{
    private readonly UserRepository _userRepository;
    private List<User> _users;

    public FormManageUsers(UserRepository userRepository)
    {
        InitializeComponent();
        
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _users = new List<User>();

        // Устанавливаем свойство, которое не нашлось в дизайнере
        dataGridView.AutoGenerateColumns = false;

        LoadUsers();
    }

    /// <summary>
    /// Загружает список пользователей из БД и отображает в таблице.
    /// </summary>
    private void LoadUsers()
    {
        _users = _userRepository.GetAllUsers();
        
        // Сбрасываем источник данных перед новой привязкой
        dataGridView.DataSource = null;
        dataGridView.DataSource = _users;
        
        // Снимаем выделение с первой строки после загрузки
        dataGridView.ClearSelection();
    }

    /// <summary>
    /// Возвращает выбранного в таблице пользователя.
    /// </summary>
    private User? GetSelectedUser()
    {
        if (dataGridView.CurrentRow is null || dataGridView.CurrentRow.IsNewRow)
            return null;

        var idValue = dataGridView.CurrentRow.Cells["ColumnId"].Value;
        if (idValue is null || idValue == DBNull.Value)
            return null;

        var selectedId = Convert.ToInt32(idValue);
        return _users.FirstOrDefault(u => u.Id == selectedId);
    }

    #region Обработчики кнопок

    private void buttonRefresh_Click(object? sender, EventArgs e)
    {
        LoadUsers();
    }

    private void buttonAdd_Click(object? sender, EventArgs e)
    {
        using var createUserForm = new FormCreateUser(_userRepository);
        if (createUserForm.ShowDialog() == DialogResult.OK)
        {
            LoadUsers();
        }
    }

    private void buttonBlock_Click(object? sender, EventArgs e)
    {
        var user = GetSelectedUser();
        if (user is null)
        {
            MessageBox.Show("Выберите пользователя в таблице.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (user.IsAdmin)
        {
            MessageBox.Show("Нельзя заблокировать администратора.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var newLockedState = !user.IsLocked;
        var action = newLockedState ? "заблокировать" : "разблокировать";

        var result = MessageBox.Show(
            $"Вы уверены, что хотите {action} пользователя \"{user.Username}\"?",
            "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
            return;

        _userRepository.SetLocked(user.Username, newLockedState);
        LoadUsers();
    }

    private void buttonEdit_Click(object? sender, EventArgs e)
    {
        var user = GetSelectedUser();
        if (user is null)
        {
            MessageBox.Show("Выберите пользователя в таблице.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var editUserForm = new FormEditUser(_userRepository, user);
        if (editUserForm.ShowDialog() == DialogResult.OK)
        {
            LoadUsers();
        }
    }

    private void buttonDelete_Click(object? sender, EventArgs e)
    {
        var user = GetSelectedUser();
        if (user is null)
        {
            MessageBox.Show("Выберите пользователя в таблице.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (user.IsAdmin)
        {
            MessageBox.Show("Нельзя удалить администратора.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show(
            $"Вы уверены, что хотите удалить пользователя \"{user.Username}\"?",
            "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        if (_userRepository.DeleteUser(user.Username))
        {
            LoadUsers();
        }
        else
        {
            MessageBox.Show("Не удалось удалить пользователя.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion

}