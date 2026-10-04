using CyberSecurityApplication.Db;

namespace CyberSecurityApplication.Forms;

using System;
using System.Windows.Forms;

public partial class FormMain : Form
{
    private readonly DatabaseManager _dbManager;
    private readonly UserRepository _userRepository;
    private readonly User _currentUser;

    public FormMain(DatabaseManager dbManager, UserRepository userRepository, User currentUser)
    {
        InitializeComponent();
        
        _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

        SetupMenu();
    }

    /// <summary>
    /// Настраивает меню в зависимости от роли пользователя.
    /// </summary>
    private void SetupMenu()
    {
        // Очищаем существующие элементы (на случай, если они были созданы в дизайнере)
        menuStrip.Items.Clear();

        // Вкладка для роли (только одна: либо Admin, либо User)
        if (_currentUser.IsAdmin)
        {
            SetupAdminMenu();
        }
        else
        {
            SetupUserMenu();
        }

        // Вкладка Common (всегда отображается)
        SetupCommonMenu();
    }

    /// <summary>
    /// Вкладка "Admin" с функциями управления пользователями.
    /// </summary>
    private void SetupAdminMenu()
    {
        var adminMenu = new ToolStripMenuItem("Admin");

        var changePasswordItem = new ToolStripMenuItem("Change password");
        changePasswordItem.Click += OnAdminChangePasswordClick;
        adminMenu.DropDownItems.Add(changePasswordItem);

        var manageUsersItem = new ToolStripMenuItem("Manage users");
        manageUsersItem.Click += OnManageUsersClick;
        adminMenu.DropDownItems.Add(manageUsersItem);

        menuStrip.Items.Add(adminMenu);
    }

    /// <summary>
    /// Вкладка "User" с функциями обычного пользователя.
    /// </summary>
    private void SetupUserMenu()
    {
        var userMenu = new ToolStripMenuItem("User");

        var changePasswordItem = new ToolStripMenuItem("Change password");
        changePasswordItem.Click += OnUserChangePasswordClick;
        userMenu.DropDownItems.Add(changePasswordItem);

        menuStrip.Items.Add(userMenu);
    }

    /// <summary>
    /// Вкладка "Common" с общими функциями (всегда отображается).
    /// </summary>
    private void SetupCommonMenu()
    {
        var commonMenu = new ToolStripMenuItem("Common");

        var infoItem = new ToolStripMenuItem("Info");
        infoItem.Click += OnInfoClick;
        commonMenu.DropDownItems.Add(infoItem);

        var hashFileItem = new ToolStripMenuItem("Hash File");
        hashFileItem.Click += OnSha1HashClick;
        commonMenu.DropDownItems.Add(hashFileItem);

        var exitItem = new ToolStripMenuItem("Exit");
        exitItem.Click += OnExitClick;
        commonMenu.DropDownItems.Add(exitItem);

        menuStrip.Items.Add(commonMenu);
    }

    #region Обработчики событий

    private void OnAdminChangePasswordClick(object? sender, EventArgs e)
    {
        using var changePasswordForm = new FormChangePassword(_userRepository, _currentUser);
        changePasswordForm.ShowDialog();
    }

    private void OnSha1HashClick(object? sender, EventArgs e)
    {
        using var sha1Form = new HashFile();
        sha1Form.ShowDialog();
    }

    private void OnUserChangePasswordClick(object? sender, EventArgs e)
    {
        using var changePasswordForm = new FormChangePassword(_userRepository, _currentUser);
        changePasswordForm.ShowDialog();
    }

    private void OnManageUsersClick(object? sender, EventArgs e)
    {
        using var manageUsersForm = new FormManageUsers(_userRepository);
        manageUsersForm.ShowDialog();
    }

    private void OnInfoClick(object? sender, EventArgs e)
    {
        // Пункт 18 ТЗ: информация об авторе и задании
        var message = $"Автор: Максимов Александр\n" +
                      $"Вариант: 14\n" +
                      $"Ограничение: Несовпадение с именем пользователя в обратном порядке\n" +
                      $"Шифрование: DES-CBC\n" +
                      $"Хеширование: MD5";
        MessageBox.Show(message, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void OnExitClick(object? sender, EventArgs e)
    {
        Close();
    }

    #endregion
}