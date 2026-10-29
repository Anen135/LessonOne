
using LessonOne.models;
using LessonOne.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.pages;

public class UserDetailPage : ContentPage
{
    UserDetailView vm = new();
    public UserDetailPage(User user)
    {
        Title = "Изменение пользователя";
        BindingContext = vm;
        Content = Build();
        BackgroundColor = Color.FromArgb("#2463EB");
    }

    private View Build()
    {

        Entry loginEntry = new() { Placeholder = "enter login", BackgroundColor = Color.FromArgb("F5F7FC"), TextColor = Color.FromArgb("252B3A"), HeightRequest = 48 };
        loginEntry.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserLogin), BindingMode.TwoWay);
        Entry passwordEntry = new() { Placeholder = "enter password", IsPassword = true, BackgroundColor = Color.FromArgb("F5F7FC"), TextColor = Color.FromArgb("252B3A"), PlaceholderColor = Color.FromArgb("9AA3B2"), HeightRequest = 48 };
        passwordEntry.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserPassword), BindingMode.TwoWay);
        Entry emailEntry = new() { Placeholder = "enter email", Keyboard = Keyboard.Email, BackgroundColor = Color.FromArgb("F5F7FC"), TextColor = Color.FromArgb("252B3A"), PlaceholderColor = Color.FromArgb("9AA3B2"), HeightRequest = 48 };
        emailEntry.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserEmail), BindingMode.TwoWay);
        Label welcomeText = new()
        {
            Text = "Manage your personal information",
            FontSize = 15,
            TextColor = Color.FromArgb("#DCE7FF"),
            HorizontalTextAlignment = TextAlignment.Center
        };
        Button profileButton = new()
        {
            BackgroundColor = Colors.Transparent,
            TextColor = Colors.White,
            BorderColor = Colors.White,
            BorderWidth = 1.5,
            CornerRadius = 22,
            HeightRequest = 45
        };
        Label formTitle = new()
        {
            Text = "edit profile",
            FontSize = 27,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#3974E8"),
            HorizontalOptions = LayoutOptions.Center
        };
        return new StackLayout
        { 
            loginEntry, passwordEntry, emailEntry, welcomeText, profileButton, formTitle
        };
    }
}
