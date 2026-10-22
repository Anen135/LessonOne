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
        var usrs = DBService.Connection.Table<User>().ToList();
        foreach (var user in usrs)
        {
            Users.Add(user);
        }
    }
}
