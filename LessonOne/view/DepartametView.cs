using CommunityToolkit.Mvvm.ComponentModel;
using LessonOne.models;
using LessonOne.services;
using System.Collections;
using System.Collections.ObjectModel;

namespace LessonOne.view
{
    internal class DepartametView : ObservableObject
    {
        public ObservableCollection<Departament> Departaments { get; internal set; } = new();

        public void Load()
        {
            var departanebt = AppDataContext.db.Departament.ToList();

        }
        //foreach (var deoartament in departaments)
        //{
        //    Departaments.Add(departament);
        //}


}
}