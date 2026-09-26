using CyberSecurityApplication.Crypto;
using Microsoft.Data.Sqlite;

namespace CyberSecurityApplication.Db;

using System;
using System.IO;

public sealed class DatabaseManager : IDisposable
{
    private const string EncryptedDbFileName = "users.db.enc";

    private SqliteConnection? _connection;
    private string? _tempDbPath;
    private bool _isInitialized;
    private bool _disposed;

    public bool IsInitialized => _isInitialized;

    /// <summary>
    /// Инициализирует БД: расшифровывает файл, создаёт временный файл, подключается.
    /// </summary>
    /// <param name="passphrase">Парольная фраза для расшифровки.</param>
    /// <returns>True, если инициализация успешна (парольная фраза верна).</returns>
    public bool Initialize(string passphrase)
    {
        ThrowIfDisposed();

        if (_isInitialized)
            throw new InvalidOperationException("Database is already initialized.");

        // При первом запуске файл не существует — создаём новую БД
        if (!File.Exists(EncryptedDbFileName))
        {
            CreateInitialDatabase(passphrase);
        }

        var encryptedData = File.ReadAllBytes(EncryptedDbFileName);
        var key = EncryptionService.DeriveKey(passphrase);

        // Пытаемся расшифровать. Если не удалось — неверная парольная фраза.
        if (!EncryptionService.TryDecrypt(encryptedData, key, out var decryptedData))
        {
            return false;
        }

        try
        {
            // Создаём временный файл с расшифрованной БД
            _tempDbPath = Path.Combine(Path.GetTempPath(), $"users_{Guid.NewGuid():N}.db");
            File.WriteAllBytes(_tempDbPath, decryptedData);

            // Открываем подключение к временному файлу
            _connection = new SqliteConnection($"Data Source={_tempDbPath}");
            _connection.Open();

            // Проверяем наличие ADMIN (пункт 14 ТЗ)
            if (!CheckAdminExists())
            {
                CleanupTemporaryResources();
                return false;
            }

            _isInitialized = true;
            return true;
        }
        catch
        {
            CleanupTemporaryResources();
            throw;
        }
        finally
        {
            // Очищаем расшифрованные данные из памяти в любом случае,
            // даже если произошла ошибка
            Array.Clear(decryptedData, 0, decryptedData.Length);
        }
    }

    /// <summary>
    /// Сохраняет изменения, шифрует БД, удаляет временный файл.
    /// </summary>
    /// <param name="passphrase">Парольная фраза для шифрования.</param>
    public void SaveAndClose(string passphrase)
    {
        ThrowIfDisposed();

        if (!_isInitialized)
            throw new InvalidOperationException("Database is not initialized.");

        try
        {
            // Закрываем подключение
            if (_connection is not null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }

            // Освобождаем пул подключений, чтобы файл можно было удалить.
            // Без этого SQLite может держать файл открытым даже после Close().
            SqliteConnection.ClearAllPools();

            // Читаем временный файл в память
            var databaseBytes = File.ReadAllBytes(_tempDbPath!);

            // Шифруем
            var key = EncryptionService.DeriveKey(passphrase);
            var encryptedData = EncryptionService.Encrypt(databaseBytes, key);

            // Перезаписываем зашифрованный файл (старое содержимое стирается)
            File.WriteAllBytes(EncryptedDbFileName, encryptedData);

            // Очищаем чувствительные данные из памяти
            Array.Clear(databaseBytes, 0, databaseBytes.Length);
            Array.Clear(encryptedData, 0, encryptedData.Length);
        }
        finally
        {
            // Безопасно удаляем временный файл в любом случае
            CleanupTemporaryResources();
        }
    }

    /// <summary>
    /// Возвращает активное подключение к БД (для репозитория).
    /// </summary>
    public SqliteConnection GetConnection()
    {
        ThrowIfDisposed();

        if (!_isInitialized || _connection is null)
            throw new InvalidOperationException("Database is not initialized.");

        return _connection;
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        CleanupTemporaryResources();
        _disposed = true;
    }

    private void CreateInitialDatabase(string passphrase)
    {
        // Используем локальную переменную, чтобы не затирать _tempDbPath
        var initTempPath = Path.Combine(Path.GetTempPath(), $"users_init_{Guid.NewGuid():N}.db");

        try
        {
            using (var connection = new SqliteConnection($"Data Source={initTempPath}"))
            {
                connection.Open();
                CreateUsersTable(connection);
                InsertAdminUser(connection);
            }

            // Освобождаем пул подключений перед чтением файла
            SqliteConnection.ClearAllPools();

            // Читаем созданную БД в память и шифруем
            var databaseBytes = File.ReadAllBytes(initTempPath);
            var key = EncryptionService.DeriveKey(passphrase);
            var encryptedData = EncryptionService.Encrypt(databaseBytes, key);

            File.WriteAllBytes(EncryptedDbFileName, encryptedData);

            // Очищаем чувствительные данные из памяти
            Array.Clear(databaseBytes, 0, databaseBytes.Length);
            Array.Clear(encryptedData, 0, encryptedData.Length);
        }
        finally
        {
            // Удаляем временный файл создания
            SecureDelete(initTempPath);
        }
    }

    private static void CreateUsersTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT UNIQUE NOT NULL,
                PasswordHash TEXT NOT NULL DEFAULT '',
                Salt TEXT NOT NULL DEFAULT '',
                IsLocked INTEGER NOT NULL DEFAULT 0,
                MinPasswordLength INTEGER NOT NULL DEFAULT 0,
                PasswordDurationMonths INTEGER NOT NULL DEFAULT 0,
                PasswordRestrictionsEnabled INTEGER NOT NULL DEFAULT 0,
                LastPasswordChangeDate TEXT
            )";
        command.ExecuteNonQuery();
    }

    private static void InsertAdminUser(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Users (Username, PasswordHash, Salt, IsLocked, 
                              MinPasswordLength, PasswordDurationMonths, 
                              PasswordRestrictionsEnabled, LastPasswordChangeDate)
            VALUES ('ADMIN', '', '', 0, 0, 0, 0, NULL)";
        command.ExecuteNonQuery();
    }

    private bool CheckAdminExists()
    {
        using var command = _connection!.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = 'ADMIN'";
        var count = (long)command.ExecuteScalar()!;
        return count > 0;
    }

    private void CleanupTemporaryResources()
    {
        if (_connection is not null)
        {
            try
            {
                _connection.Close();
                _connection.Dispose();
            }
            catch
            {
                // Игнорируем ошибки при закрытии
            }
            _connection = null;
        }

        // Освобождаем пул подключений, чтобы файл можно было удалить
        SqliteConnection.ClearAllPools();

        if (_tempDbPath is not null)
        {
            SecureDelete(_tempDbPath);
            _tempDbPath = null;
        }

        _isInitialized = false;
    }

    /// <summary>
    /// Безопасно удаляет файл: перезаписывает нулями перед удалением (пункт 16 ТЗ).
    /// </summary>
    private static void SecureDelete(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        try
        {
            var fileInfo = new FileInfo(filePath);
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                var zeros = new byte[fileInfo.Length];
                stream.Write(zeros, 0, zeros.Length);
            }

            File.Delete(filePath);
        }
        catch
        {
            // Если не удалось безопасно удалить — пробуем просто удалить
            try
            {
                File.Delete(filePath);
            }
            catch
            {
                // Игнорируем, если файл заблокирован другим процессом
            }
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(DatabaseManager));
    }
}