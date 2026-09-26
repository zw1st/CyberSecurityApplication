using CyberSecurityApplication.Db;

namespace CyberSecurityApplication.Forms;

public partial class FormSignIn : Form
{
    private readonly DatabaseManager _dbManager;
    private readonly UserRepository _userRepository;

    private bool _isDatabaseDecrypted;
    private int _failedPasswordAttempts;
    private const int MaxAttempts = 3;
    
    public User? CurrentUser { get; private set; }
    public string Passphrase { get; private set; } = string.Empty;

    
    public FormSignIn(DatabaseManager dbManager, UserRepository userRepository)
    {
        InitializeComponent();
        
        _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        
        _isDatabaseDecrypted = false;
        _failedPasswordAttempts = 0;
    }
    
    private void buttonClear_Click(object sender, EventArgs e)
    {
        textBoxLogin.Text = "";
        textBoxPassword.Text = "";
        textBoxPhrase.Text = "";
    }

    private void buttonEnter_Click(object sender, EventArgs e)
    {
        // Шаг 1: Расшифровка БД (только один раз)
        if (!_isDatabaseDecrypted)
        {
            if (!TryDecryptDatabase())
                return; // При неверной фразе программа уже завершена
        }

        // Шаг 2: Проверка логина
        var username = textBoxLogin.Text.Trim();
        if (string.IsNullOrEmpty(username))
        {
            MessageBox.Show("Введите имя пользователя.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxLogin.Focus();
            return;
        }

        var user = _userRepository.GetUserByUsername(username);
        
        // Пункт 6 ТЗ: пользователь не найден → повторный ввод имени
        if (user is null)
        {
            MessageBox.Show(
                "Пользователь не найден. Повторите ввод имени или завершите работу.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxLogin.Clear();
            textBoxLogin.Focus();
            return;
        }

        // Проверка блокировки
        if (user.IsLocked)
        {
            MessageBox.Show("Учётная запись заблокирована. Обратитесь к администратору.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxLogin.Clear();
            textBoxLogin.Focus();
            return;
        }

        // Шаг 3: Проверка пароля
        var password = textBoxPassword.Text;
        if (!_userRepository.VerifyPassword(username, password))
        {
            _failedPasswordAttempts++;
            
            // Пункт 7 ТЗ: три неудачные попытки → завершение
            if (_failedPasswordAttempts >= MaxAttempts)
            {
                MessageBox.Show("Превышено количество попыток ввода пароля. Программа завершается.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
                Close();
                return;
            }

            var remaining = MaxAttempts - _failedPasswordAttempts;
            MessageBox.Show($"Неверный пароль. Осталось попыток: {remaining}.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxPassword.Clear();
            textBoxPassword.Focus();
            return;
        }

        // Шаг 4: Успешный вход
        CurrentUser = user;
        DialogResult = DialogResult.OK;
        Close();
    }
    
    /// <summary>
    /// Пытается расшифровать БД с помощью парольной фразы.
    /// При неудаче завершает программу (пункт 15 ТЗ).
    /// </summary>
    private bool TryDecryptDatabase()
    {
        var passphrase = textBoxPhrase.Text;

        // Пункт 15 ТЗ: отказ от ввода фразы → завершение
        if (string.IsNullOrEmpty(passphrase))
        {
            MessageBox.Show("Парольная фраза не может быть пустой.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Abort;
            Close();
            return false;
        }

        // Пытаемся расшифровать БД
        if (!_dbManager.Initialize(passphrase))
        {
            // Пункт 15 ТЗ: неверная фраза → завершение
            MessageBox.Show("Неверная парольная фраза. Программа завершается.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Abort;
            Close();
            return false;
        }

        // Успешная расшифровка
        _isDatabaseDecrypted = true;
        Passphrase = passphrase;

        // Блокируем поле парольной фразы — оно больше не нужно
        textBoxPhrase.Enabled = false;

        return true;
    }
}
