using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonOne.models;
using LessonOne.services;

namespace LessonOne.view;

public partial class LoginView : ObservableObject
{
    private string login = string.Empty;

    private string password = string.Empty;

    private string message = string.Empty;

    public string Login
    {
        get => login;
        set => SetProperty(ref login, value);
    }

    public string Password
    {
        get => password;
        set => SetProperty(ref password, value);
    }

    public string Message
    {
        get => message;
        set => SetProperty(ref message, value);
    }

    private readonly Func<User, Task> onSuccess;

    public LoginView(Func<User, Task> onSuccess)
    {
        this.onSuccess = onSuccess;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        Message = string.Empty;
        if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
        {
            Message = "Enter your login and password.";
            return;
        }

        try
        {
            var user = AppDataContext.db.GetUserByLoginAndPassword(Login.Trim(), Password);
            if (user is null)
            {
                Message = "Incorrect login or password.";
                return;
            }

            await onSuccess(user);
            Password = string.Empty;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            Message = "Unable to sign in. Please try again.";
        }
    }
}
