using LessonOne.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.services
{
    public class AppDataContext : DbContext
    {
        public static AppDataContext db = null;
        public static User CurrentUser = null;

        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db");
        }
        public static void Init()
        {
            if (db != null) return;
            db = new();
            db.Database.EnsureCreated();
        }
        public List<User> GetUsers() => db.Users.ToList();
        public User GetUserById(int id) => db.Users.FirstOrDefault(u => u.Id == id);

        public void AddUser(User newUser) => db.Users.Add(newUser);
        public void SaveChanges() => db.SaveChanges();

    }
}
