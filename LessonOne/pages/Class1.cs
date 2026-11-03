using LessonOne.models;
using LessonOne.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.pages;

class DepartamentDetailPage : ContentPage
{
    Departament dep;
    Entry NameEntry = new();
    Button SaveButton = new();

    public DepartamentDetailPage(Departament departament)
    {
        Content = Build();
        dep = departament;
    }

    View Build()
    {
        SaveButton.Clicked += SaveEntry;
        return new StackLayout() {
            new Label { Text = "Name"},
            NameEntry,
            SaveButton
        };
    }

    private void SaveEntry(object? sender, EventArgs e)
    {
        dep.Name = NameEntry.Text;
        AppDataContext.db.Departament.Update(dep);
        AppDataContext.db.SaveChanges();
    }
}
