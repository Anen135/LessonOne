using LessonOne.models;
using LessonOne.services;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.pages;

public class RegisterPage : ContentPage
{
    Entry loginEntry = new();
    Entry passwordEntry = new();
    Entry confirmPasswordEntry = new();
    Entry emailEntry = new();
    Picker accessPicker = new() { ItemsSource = Enum.GetValues<UserAccess>() };
    Label message = new();

    public RegisterPage()
    {
        Title = "Registation";
        BackgroundColor = Color.FromArgb("#2463EB");
        Content = Build();
    }

    View Build()
    {
        loginEntry.Placeholder = "Login";
        passwordEntry.Placeholder = "Password";
        passwordEntry.IsPassword = true;
        confirmPasswordEntry.Placeholder = "Confirm password";
        confirmPasswordEntry.IsPassword = true;
        emailEntry.Placeholder = "Email";
        emailEntry.Keyboard = Keyboard.Email;
        StyleingEntry(loginEntry);
        StyleingEntry(passwordEntry);
        StyleingEntry(emailEntry);
        StyleingEntry(confirmPasswordEntry);
        Label formTitle = new() { Text ="Create account", FontSize = 27, FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#3974E8"), HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0,0,0,15)};
        Label welcomeTitle = new() { Text = "Get started", FontSize = 38, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, HorizontalOptions = LayoutOptions.Center };
        Label welcomeText = new() { Text = "Already have an account?", FontSize = 15, TextColor = Color.FromRgba("#DCE7FF"), HorizontalOptions = LayoutOptions.Center };
        Button loginButton = new() { Text = "Log in", WidthRequest = 150, HeightRequest = 45, BackgroundColor = Colors.Transparent, TextColor = Colors.White, BorderColor = Colors.White, BorderWidth = 1.5, CornerRadius = 22 };

        Label accesspic = new Label() { Text = "Choose access level"};




        Button registerButton = new()
        {
            Text = "Sign up",
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Color.FromArgb("#3867F5"),
            CornerRadius = 8,
            TextColor = Colors.White,
            HeightRequest = 48
        };
        registerButton.Clicked += OnRegister;
        message.FontSize = 13;
        message.HorizontalOptions = LayoutOptions.Center;
        Border formCard = new()
        {
            BackgroundColor = Colors.White,
            Stroke = Colors.Transparent, 
            StrokeShape = new RoundRectangle { CornerRadius = 18},
            Padding = new Thickness(30,35), 
            Shadow = new Shadow { Brush = Colors.Black, Offset = new Point(0,5), Radius =15,Opacity= 0.15f},
            Content = new VerticalStackLayout { Spacing = 15, Children = {formTitle, new Label{Text = "email",FontSize = 13, TextColor = Color.FromArgb("#555555")},emailEntry, new Label { Text = "Password", FontSize =13, TextColor = Color.FromArgb("#555555") }, passwordEntry, new Label { Text = "confirm password",FontSize=13,TextColor= Color.FromArgb("#555555")}, confirmPasswordEntry, new Label { Text = "full name", FontSize = 13, TextColor = Color.FromArgb("#555555") }, loginEntry, registerButton,message }
            }
        };
        Border leftPanel = new()
        {
            BackgroundColor = Color.FromArgb("#3867F5"),
            Stroke = Colors.Transparent,
            StrokeShape = new RoundRectangle { CornerRadius = 18 },
            Padding = 25,
            Content = new VerticalStackLayout { Spacing = 20, VerticalOptions = LayoutOptions.Center, Children = { welcomeText, welcomeTitle, loginButton } }
        };
        Grid mainGrid = new()
        {
            ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(1.1, GridUnitType.Star) } },
            ColumnSpacing = 20,
            Padding = 25,
            Children = { leftPanel, formCard }
        };
        Grid.SetColumn(leftPanel, 0);
        Grid.SetColumn(formCard, 1);
        
        return new StackLayout
        {
            Padding = 25,
            Spacing = 15,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new Label{Text = "account creation",FontSize =26, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center},
                loginEntry, passwordEntry, confirmPasswordEntry, emailEntry, accessPicker, registerButton,message, formCard,leftPanel,mainGrid
            }
        };
    }

    private void OnRegister(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(loginEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text) || string.IsNullOrWhiteSpace(confirmPasswordEntry.Text) || string.IsNullOrWhiteSpace(emailEntry.Text) || accessPicker.SelectedItem == null)
        {
            message.Text = "Fill all fields!";
            message.TextColor = Colors.Red;
        }else if(passwordEntry.Text != confirmPasswordEntry.Text)
        {
            message.Text = "passwords do not match";
            message.TextColor = Colors.Red;
        }
        else { 
            message.Text = "registration was successful";
            AppDataContext.db.AddUser(new User { Email = emailEntry.Text, Access = (UserAccess)accessPicker.SelectedItem, Password = passwordEntry.Text, Login = loginEntry.Text });
            AppDataContext.db.SaveChanges();
        }
    }

private Entry StyleingEntry(Entry entry)
    {
        entry.BackgroundColor = Color.FromArgb("F5F7FC");
        entry.TextColor = Color.FromArgb("252B3A");
        entry.PlaceholderColor = Color.FromArgb("9AA3B3A");
        entry.HeightRequest = 45;
        entry.Margin = new Thickness(0, 0, 0, 5);
        return entry;
    }
}
