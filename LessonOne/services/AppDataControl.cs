using LessonOne.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LessonOne.services
{

    public class AppDataControl : DbContext
    {
        public static AppDataControl db = null;
        public DbSet<User> Users => Set<User>();

        public DbSet<Projects> Projects => Set<Projects>();

        public DbSet<Departament> Departament => Set<Departament>();
        public static void Init()
        {
            if (db != null) return;
            db = new();
            db.Database.EnsureCreated();
        }

        public List<User> GetUsers() => db.Users.ToList();

        public List<Departament> GetDepartament() => db.Departament.ToList();

        public User GetUserById(int id) => db.Users.FirstOrDefault(u => u.Id == id);
        public void AdUser(User newUser) => db.Users.Add(newUser);
        public void SaveChanges() => base.SaveChanges();


        public void AdProject(Projects newProjects) => db.Projects.Add(newProjects);

        public void AdDepartament(Departament newDepartament) => db.Departament.Add(newDepartament);


    }


}
