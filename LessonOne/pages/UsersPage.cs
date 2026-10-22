using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Markup;
using LessonOne.models;
using LessonOne.view;

namespace LessonOne.pages;

public class UsersPage : ContentPage
{
    UserView vm;
    public UsersPage()
    {
        vm = new UserView();
        vm.Load();
        BindingContext = vm;
        Content = Build();
    }

    View Build()
    {
        CollectionView UserCollection = new CollectionView()
        {
            ItemsSource = vm.Users,
        };

        UserCollection.ItemTemplate = new DataTemplate(() =>
        {
            return new HorizontalStackLayout
            {
                new Label { Text = "Login: " },
                new Label {Margin = new Thickness {Right = 15}}.Bind(Label.TextProperty, nameof(User.Login)),
                new Label { Text = "Password: " },
                new Label {Margin = new Thickness {Right = 15}}.Bind(Label.TextProperty, nameof(User.Password))
            };
        });
        return new StackLayout {
            UserCollection
        };
    }
}
