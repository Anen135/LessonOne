using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Markup;
using LessonOne.models;

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
        Content = Build();
    }

    View Build()
    {
        



        return new StackLayout
        {
            Children = { BuildLogo(), BuildLoginPanel() },
            Spacing = 5
            
        };
    }

    private IView BuildLoginPanel()
    {
        loginEntry.WidthRequest = 10;
        loginEntry.HeightRequest = 10;
        passwordEntry.WidthRequest = 10;
        passwordEntry.HeightRequest = 10;

        return new StackLayout
        {
            loginEntry, passwordEntry, confirmPasswordEntry, emailEntry, accessPicker, message
        };
    }

    private IView BuildLogo()
    {
        return new StackLayout();
    }
    
}
