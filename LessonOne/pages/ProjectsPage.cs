using LessonOne.models;
using LessonOne.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.pages;

public class ProjectsPage : ContentPage
{
    public ProjectsPage()
    {
        Content = Build();
    }
    private View Build()
    {
        var deps = DBService.Connection.Table<Projects>().ToList();
        var CollectionDeps = new CollectionView();
        CollectionDeps.ItemsSource = deps;
        CollectionDeps.ItemTemplate = new DataTemplate(() =>
        {
            var depName = new Label() { Margin = 40 };
            depName.SetBinding(Label.TextProperty, nameof(Projects.Name));
            return new StackLayout { depName };
        });
        return CollectionDeps;
    }
}
