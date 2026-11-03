using LessonOne.models;
using LessonOne.services;
using System.Collections.ObjectModel;

namespace LessonOne.pages;

internal class DepartametView
{
    public ObservableCollection<Departament> Departaments { get; } = new();

    public void Load()
    {
        var departments = AppDataContext.db.GetDepartament();
        Departaments.Clear();
        foreach (var department in departments) Departaments.Add(department);
    }
}
