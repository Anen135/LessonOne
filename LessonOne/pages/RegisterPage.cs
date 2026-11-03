using System.Net.Mail;
using LessonOne.models;
using LessonOne.services;
using Microsoft.Maui.Controls.Shapes;

namespace LessonOne.pages;

public class RegisterPage : ContentPage
{
    private readonly Entry loginEntry = new() { Placeholder = "Login", AutomationId = "RegisterLoginEntry" };
    private readonly Entry passwordEntry = new() { Placeholder = "Password", IsPassword = true, AutomationId = "RegisterPasswordEntry" };
    private readonly Entry confirmPasswordEntry = new() { Placeholder = "Confirm password", IsPassword = true, AutomationId = "ConfirmPasswordEntry" };
    private readonly Entry emailEntry = new() { Placeholder = "Email", Keyboard = Keyboard.Email, AutomationId = "EmailEntry" };
    private readonly Picker accessPicker = new()
    {
        Title = "Access level",
        ItemsSource = Enum.GetValues<UserAccess>(),
        SelectedItem = UserAccess.Low,
        TextColor = Color.FromArgb("#252B3A"),
        BackgroundColor = Color.FromArgb("#F5F7FC"),
        TitleColor = Color.FromArgb("#555555")
    };
    private readonly Label message = new() { FontSize = 13, TextColor = Colors.DarkRed };
    private readonly Button registerButton = new() { Text = "Sign up", AutomationId = "RegisterButton" };

    public RegisterPage()
    {
        Title = "Create account";
        BackgroundColor = Color.FromArgb("#2463EB");
        Content = Build();
    }

    private View Build()
    {
        foreach (var entry in new[] { loginEntry, passwordEntry, confirmPasswordEntry, emailEntry })
        {
            entry.BackgroundColor = Color.FromArgb("#F5F7FC");
            entry.TextColor = Color.FromArgb("#252B3A");
            entry.PlaceholderColor = Color.FromArgb("#9AA3B3");
            entry.HeightRequest = 48;
        }

        registerButton.Clicked += OnRegister;
        var loginButton = new Button { Text = "Already have an account? Log in" };
        loginButton.Clicked += async (_, _) => await Shell.Current.GoToAsync("..");

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
                        Text = "Get started",
                        TextColor = Colors.White,
                        FontSize = 32,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center
                    },
                    new Border
                    {
                        BackgroundColor = Colors.White,
                        Stroke = Colors.Transparent,
                        StrokeShape = new RoundRectangle { CornerRadius = 18 },
                        Padding = 28,
                        Content = new VerticalStackLayout
                        {
                            Spacing = 16,
                            Children =
                            {
                                new Label
                                {
                                    Text = "Create account",
                                    FontSize = 26,
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.FromArgb("#3974E8")
                                },
                                loginEntry,
                                emailEntry,
                                passwordEntry,
                                confirmPasswordEntry,
                                accessPicker,
                                message,
                                registerButton,
                                loginButton
                            }
                        }
                    }
                }
            }
        };
    }

    private async void OnRegister(object? sender, EventArgs e)
    {
        message.Text = string.Empty;
        var login = loginEntry.Text?.Trim() ?? string.Empty;
        var email = emailEntry.Text?.Trim() ?? string.Empty;
        var password = passwordEntry.Text ?? string.Empty;
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)
            || string.IsNullOrWhiteSpace(confirmPasswordEntry.Text) || string.IsNullOrWhiteSpace(email)
            || accessPicker.SelectedItem is not UserAccess access)
        {
            message.Text = "Fill in all fields.";
            return;
        }

        if (password != confirmPasswordEntry.Text)
        {
            message.Text = "Passwords do not match.";
            return;
        }

        if (!MailAddress.TryCreate(email, out var address) || address.Address != email)
        {
            message.Text = "Enter a valid email address.";
            return;
        }

        registerButton.IsEnabled = false;
        try
        {
            if (AppDataContext.db.LoginExists(login))
            {
                message.Text = "This login is already registered.";
                return;
            }

            var user = new User { Login = login, Password = password, Email = email, Access = access };
            AppDataContext.db.AddUser(user);
            try
            {
                AppDataContext.db.SaveChanges();
            }
            catch
            {
                AppDataContext.db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                throw;
            }

            passwordEntry.Text = string.Empty;
            confirmPasswordEntry.Text = string.Empty;
            await DisplayAlert("Account created", "You can now log in with your login and password.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            message.Text = "Unable to create the account. Please try again.";
        }
        finally
        {
            registerButton.IsEnabled = true;
        }
    }
}
