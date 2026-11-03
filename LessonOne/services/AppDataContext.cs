using LessonOne.models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LessonOne.services;

public class AppDataContext : DbContext
{
    private static AppDataContext? database;

    public static AppDataContext db => database
        ?? throw new InvalidOperationException("The database has not been initialized.");

    public static User? CurrentUser { get; set; }

    public DbSet<User> Users => Set<User>();

    public AppDataContext()
    {
    }

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = Path.Combine(FileSystem.AppDataDirectory, "app.db")
            }.ToString();
            optionsBuilder.UseSqlite(connectionString);
        }
    }

    public static void Init()
    {
        if (database is not null)
        {
            return;
        }

        MigrateLegacyDatabase(
            Path.Combine(FileSystem.AppDataDirectory, "app.db"),
            Path.Combine(Environment.CurrentDirectory, "app.db"),
            Path.Combine(AppContext.BaseDirectory, "app.db"));

        var context = new AppDataContext();
        try
        {
            context.Database.EnsureCreated();
            database = context;
        }
        catch
        {
            context.Dispose();
            throw;
        }
    }

    internal static void MigrateLegacyDatabase(string destinationPath, params string[] legacyPaths)
    {
        destinationPath = Path.GetFullPath(destinationPath);
        if (File.Exists(destinationPath))
        {
            return;
        }

        var comparer = OperatingSystem.IsWindows() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
        var checkedPaths = new HashSet<string>(comparer) { destinationPath };
        foreach (var candidate in legacyPaths)
        {
            var sourcePath = Path.GetFullPath(candidate);
            if (!checkedPaths.Add(sourcePath) || !File.Exists(sourcePath))
            {
                continue;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            var temporaryPath = destinationPath + ".migrating-" + Guid.NewGuid().ToString("N");
            try
            {
                using (var source = new SqliteConnection(new SqliteConnectionStringBuilder
                {
                    DataSource = sourcePath,
                    Mode = SqliteOpenMode.ReadOnly,
                    Pooling = false
                }.ToString()))
                using (var destination = new SqliteConnection(new SqliteConnectionStringBuilder
                {
                    DataSource = temporaryPath,
                    Pooling = false
                }.ToString()))
                {
                    source.Open();
                    destination.Open();
                    source.BackupDatabase(destination);
                    using var command = destination.CreateCommand();
                    command.CommandText = "PRAGMA journal_mode=DELETE;";
                    command.ExecuteNonQuery();
                }

                File.Move(temporaryPath, destinationPath, overwrite: false);
                return;
            }
            finally
            {
                File.Delete(temporaryPath);
            }
        }
    }

    public List<User> GetUsers() => Users.OrderBy(user => user.Login).ToList();

    public User? GetUserById(int id) => Users.FirstOrDefault(user => user.Id == id);

    public User? GetUserByLoginAndPassword(string login, string password) =>
        Users.FirstOrDefault(user => user.Login.ToLower() == login.ToLower() && user.Password == password);

    public bool LoginExists(string login) =>
        Users.Any(user => user.Login.ToLower() == login.ToLower());

    public void AddUser(User newUser)
    {
        if (LoginExists(newUser.Login))
        {
            throw new InvalidOperationException("This login is already registered.");
        }

        Users.Add(newUser);
    }
}
