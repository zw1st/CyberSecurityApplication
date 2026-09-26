using CyberSecurityApplication.Db;
using CyberSecurityApplication.Forms;

namespace CyberSecurityApplication
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Создаём менеджер БД (будет удалён автоматически благодаря using)
            using var dbManager = new DatabaseManager();
            var userRepository = new UserRepository(dbManager);

            // Показываем форму входа (модальное окно)
            using var signInForm = new FormSignIn(dbManager, userRepository);
            var result = signInForm.ShowDialog();

            // Если пользователь не вошёл (отмена или ошибка) — выходим
            if (result != DialogResult.OK)
            {
                return;
            }

            // Получаем данные после успешного входа
            var currentUser = signInForm.CurrentUser!;
            var passphrase = signInForm.Passphrase;
            
            using var mainForm = new FormMain(dbManager, userRepository, currentUser);

            
            //ApplicationConfiguration.Initialize();
            Application.Run(mainForm);
            
            if (dbManager.IsInitialized)
            {
                dbManager.SaveAndClose(passphrase);
            }
        }
    }
}