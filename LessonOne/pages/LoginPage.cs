using LessonOne.models;
using LessonOne.services;
using LessonOne.view;
using Microsoft.Maui.Controls.Shapes;

namespace LessonOne.pages;

public class LoginPage : ContentPage
{
    private readonly LoginView vm;

    public LoginPage()
    {
        Title = "Log in";
        BackgroundColor = Color.FromArgb("#2463EB");
        vm = new LoginView(SignInAsync);
        BindingContext = vm;
        Content = Build();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.Password = string.Empty;
        vm.Message = string.Empty;
    }

    private View Build()
    {
        var usernameEntry = new Entry
        {
            Placeholder = "Login",
            AutomationId = "LoginEntry",
            ReturnType = ReturnType.Next
        };
        var passwordEntry = new Entry
        {
            Placeholder = "Password",
            IsPassword = true,
            AutomationId = "PasswordEntry",
            ReturnType = ReturnType.Go
        };
        foreach (var entry in new[] { usernameEntry, passwordEntry })
        {
            entry.BackgroundColor = Color.FromArgb("#F5F7FC");
            entry.TextColor = Color.FromArgb("#252B3A");
            entry.PlaceholderColor = Color.FromArgb("#9AA3B3");
            entry.HeightRequest = 48;
        }

        usernameEntry.SetBinding(Entry.TextProperty, nameof(LoginView.Login));
        passwordEntry.SetBinding(Entry.TextProperty, nameof(LoginView.Password));
        usernameEntry.Completed += (_, _) => passwordEntry.Focus();
        passwordEntry.Completed += async (_, _) => await vm.LoginCommand.ExecuteAsync(null);

        var messageLabel = new Label { TextColor = Colors.DarkRed, FontSize = 13 };
        messageLabel.SetBinding(Label.TextProperty, nameof(LoginView.Message));
        var loginButton = new Button { Text = "Log in", AutomationId = "LoginButton" };
        loginButton.SetBinding(Button.CommandProperty, nameof(LoginView.LoginCommand));
        var registerButton = new Button { Text = "Create an account", AutomationId = "OpenRegisterButton" };
        registerButton.Clicked += async (_, _) =>
        {
            registerButton.IsEnabled = false;
            try
            {
                await Shell.Current.GoToAsync("register");
            }
            finally
            {
                registerButton.IsEnabled = true;
            }
        };

        return new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 24,
                Spacing = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                MaximumWidthRequest = 500,
                Children =
                {
                    new Label
                    {
                        Text = "Welcome back",
                        TextColor = Colors.White,
                        FontSize = 32,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center
                    },
                    new Border
                    {
                        BackgroundColor = Colors.White,
                        Stroke = Colors.Transparent,
                        StrokeShape = new  RoundRectangle { CornerRadius = 18 },
                        Padding = 28,
                        Content = new VerticalStackLayout
                        {
                            Spacing = 16,
                            Children =
                            {
                                usernameEntry,
                                passwordEntry,
                                messageLabel,
                                loginButton,
                                registerButton
                            }
                        }
                    }
                }
            }
        };
    }

    private async Task SignInAsync(User signedUser)
    {
        AppDataContext.CurrentUser = signedUser;
        try
        {
            await Shell.Current.GoToAsync("//users");
        }
        catch
        {
            AppDataContext.CurrentUser = null;
            throw;
        }
    }
}
