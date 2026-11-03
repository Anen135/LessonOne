using LessonOne.models;
using LessonOne.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.pages;

public class DepartametPage : ContentPage
{
    public DepartametPage()
    {
        Content = Build();
    }

    View Build()
    {
        var deps = AppDataContext.db.GetDepartament().ToList();
        var CollectionDeps = new CollectionView();
        CollectionDeps.ItemsSource = deps;
        CollectionDeps.ItemTemplate = new DataTemplate(() => 
        { // Departanebt Item
            Label label = new();
            Button Detail = new();
            Departament dep = new();
            Detail.Clicked += (_, __) =>
            {
                Navigation.PushAsync(new DepartamentDetailPage(dep));
            };
            StackLayout stlayout = new() { label, Detail };
            stlayout.BindingContextChanged += (s, e) =>
            {
                dep = stlayout.BindingContext as Departament;
                label.Text = dep.Name;

            };
            return stlayout;
        });
        return CollectionDeps;
    }
}
