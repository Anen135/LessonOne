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

        usernameEntry.SetBinding(Entry.TextProperty, nameof(LoginView.Login), BindingMode.TwoWay);
        passwordEntry.SetBinding(Entry.TextProperty, nameof(LoginView.Password), BindingMode.TwoWay);
        confirmPasswordEntry.SetBinding(Entry.TextProperty, nameof(LoginView.ConfirmPassword), BindingMode.TwoWay);
        messageLabel.SetBinding(Label.TextProperty, nameof(LoginView.Message), BindingMode.TwoWay);
        button.SetBinding(Button.CommandProperty, nameof(LoginView.onLoginCommand), BindingMode.TwoWay);
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
        AppDataContext.CurrentUser = signedUser;
        //Navigation.PushAsync(new UsersPage());
        //Navigation.PushAsync(new RegisterPage());
        Navigation.PushAsync(new DepartametPage());
    }
}
