using CommunityToolkit.Mvvm.ComponentModel;
using LessonOne.models;
using LessonOne.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.view;

public partial class UserDetailView : ObservableObject
{
    [ObservableProperty]
    public int userId;
    [ObservableProperty]
    public string userLogin;
    [ObservableProperty]
    public string userPassword;
    [ObservableProperty]
    public string userEmail;
    public void Save()
    {
        AppDataContext.db.AddUser(new User { Login = UserLogin, Password = userPassword, Email = userEmail, Id = userId });
    }
}
