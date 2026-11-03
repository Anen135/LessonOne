using CommunityToolkit.Mvvm.ComponentModel;
using LessonOne.models;
using LessonOne.services;
using System.Net.Mail;

namespace LessonOne.view;

public partial class UserDetailView : ObservableObject
{
    private readonly User user;
    [ObservableProperty] private string userLogin;
    [ObservableProperty] private string userPassword;
    [ObservableProperty] private string userEmail;

    public UserDetailView(User user)
    {
        this.user = user;
        userLogin = user.Login;
        userPassword = user.Password;
        userEmail = user.Email;
    }

    public void Save()
    {
        var login = UserLogin.Trim();
        var email = UserEmail.Trim();
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(UserPassword)
            || !MailAddress.TryCreate(email, out var address) || address.Address != email)
            throw new InvalidOperationException("Enter a login, password and valid email.");
        if (AppDataContext.db.Users.Any(other => other.Id != user.Id && other.Login.ToLower() == login.ToLower()))
            throw new InvalidOperationException("This login is already registered.");
        var previous = (user.Login, user.Password, user.Email);
        try
        {
            user.Login = login;
            user.Password = UserPassword;
            user.Email = email;
            AppDataContext.db.SaveChanges();
        }
        catch
        {
            (user.Login, user.Password, user.Email) = previous;
            throw;
        }
    }
}
