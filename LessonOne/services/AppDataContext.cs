using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LessonOne.models;

namespace LessonOne.services
{
    public class AppDataContext : DbContext 
    {
        public static AppDataContext db = null;
        public DbSet<User> Users => Set<User>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app2.db");
        }
        public DbSet<Projects> Projects => Set<Projects>();

        public DbSet<Departament> Departament => Set<Departament>();
        static public void Init()
        {
            if (db != null) return;
            db = new();
            db.Database.EnsureCreated();
        }
        public List<User> GetUsers()
        {   
            return db.Users.ToList();
        }
        public User GetUserById(int id) =>db.Users.FirstOrDefault(u=>u.Id==id);
        public List<Departament> GetDepartament() => db.Departament.ToList();
        public void AdProject(Projects newProjects) => db.Projects.Add(newProjects);
        public void AdDepartament(Departament newDepartament) => db.Departament.Add(newDepartament);
        public User GetUserByLoginAndPassword(string login, string password)=> db.Users.FirstOrDefault(u => u.Password==password && u.Login==login);
        public void AddUser(User newUser) =>    db.Users.Add(newUser);
        public static User CurrentUser = null;
    }
}
