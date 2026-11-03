using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LessonOne.models;
using LessonOne.services;
using LessonOne.view;

namespace LessonOne.pages
{
    public class SecondPage : ContentPage
    {
        SecondPage vm;

        public SecondPage()
        {
            vm = new SecondPage();
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

            usernameEntry.SetBinding(Entry.TextProperty, nameof(SecondView));
            passwordEntry.SetBinding(Entry.TextProperty, nameof(SecondView));

            return new StackLayout();
        }
    }
}
