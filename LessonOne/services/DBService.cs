using LessonOne.models;
using SQLite;

namespace LessonOne.services
{
    public static class DBService
    {
        public static SQLiteConnection Connection;
        public static User CurrentUser;
        public static void Init()
        {
            Connection = new(Path.Combine(FileSystem.AppDataDirectory, "db"));
            Connection.CreateTable<User>();
            Connection.CreateTable<Departament>();
            Connection.CreateTable<Projects>();
        }
    }
}
