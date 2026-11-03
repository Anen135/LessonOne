using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LessonOne.models;
using LessonOne.services;

namespace LessonOne.view;

public class UserView : ObservableObject
{
    public ObservableCollection<User> Users { get; set; } = new();
    public void Load()
    {
        //var usrs = DBService.Connection.Table<User>().ToList();
        //var users = services.AppDataControl.Table<User>();
        var usrs = AppDataControl.db.Users.ToList();

        foreach (var user in usrs)
        {
            Users.Add(user);
        }
    }
}
