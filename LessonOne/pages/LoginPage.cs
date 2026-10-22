using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LessonOne.models;
using LessonOne.services;
using LessonOne.view;

namespace LessonOne.pages;

public class LoginPage : ContentPage
{
    LoginView vm; 
    public LoginPage()
    {
        vm = new LoginView(SignIn);
        BindingContext = vm;
        Content = Build();
    }

    View Build()
    {
        var usernameEntry = new Entry();
        var passwordEntry = new Entry { IsPassword = true };
        var confirmPasswordEntry = new Entry { IsPassword = true };
        var messageLabel = new Label();
        var button = new Button { Text = "Войти" };

        usernameEntry.SetBinding(Entry.TextProperty, nameof(LoginView.Login));
        passwordEntry.SetBinding(Entry.TextProperty, nameof(LoginView.Password));
        confirmPasswordEntry.SetBinding(Entry.TextProperty, nameof(LoginView.ConfirmPassword));
        messageLabel.SetBinding(Label.TextProperty, nameof(LoginView.Message));
        button.SetBinding(Button.CommandProperty, nameof(LoginView.onLoginCommand));
        return new VerticalStackLayout
        {
            Children =
                {
                    usernameEntry,
                    passwordEntry,
                    confirmPasswordEntry,
                    messageLabel,
                    button
                }
        };

    }

    void SignIn(User signedUser)
    {
        DBService.CurrentUser = signedUser;
        Navigation.PushAsync(new UsersPage());
    }
}
