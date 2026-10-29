using LessonOne.models;
using LessonOne.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.pages;

public class DepartametPage : ContentPage
{
    public DepartametPage()
    {
        Content = Build();
    }

    private View Build()
    {
        var deps = 
        var CollectionDeps = new CollectionView();
        CollectionDeps.ItemsSource = deps;
        CollectionDeps.ItemTemplate = new DataTemplate(() =>
        {
            var depName = new Label() { Margin = 40};
            depName.SetBinding(Label.TextProperty, nameof(Departament.Name));
            return new StackLayout { depName  };
        });
        return CollectionDeps;
    }
}
