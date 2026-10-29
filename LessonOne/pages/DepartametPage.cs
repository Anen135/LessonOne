using CommunityToolkit.Maui.Markup;
using LessonOne.models;
using LessonOne.services;
using LessonOne.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LessonOne.pages;

public class DepartametPage : ContentPage
{
    DepartametView vm;
    public DepartametPage()
    {
        vm = new DepartametView();
        vm.Load();
        BindingContext = vm;
        Content = Build();
    }

    private View Build()
    {
        CollectionView DepartamentCollection = new CollectionView()
        {
            ItemsSource = vm.Departaments,
        };

        DepartamentCollection.ItemTemplate = new DataTemplate(() =>
        {
            return new HorizontalStackLayout
            {
                new Label { Margin = 40}.Bind(nameof(Departament.Name))
            };
        });
        return new StackLayout
        {
            DepartamentCollection
        };
    }
}