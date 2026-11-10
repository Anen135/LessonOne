using LessonOne.models;
using LessonOne.services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LessonOne.pages;

public class DepartamentCreatePage : ContentPage
{
    public DepartamentCreatePage()
    {
        Title = "Create department";
        Departament departament = new Departament();
        var name = new Entry {  Placeholder = "Name" };
        var employees = new Entry { Placeholder = "Enter the number of employees" };
        var message = new Label { TextColor = Colors.DarkRed };
        var save = new Button { Text = "Save" };
        save.Clicked += async (_, _) =>
        {
            
            //departament.Materials = ;
            bool IsOk = true;
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                message.Text = "Enter a department name.";
                IsOk = false;
                return;
            }
            if (string.IsNullOrWhiteSpace(employees.Text) || int.TryParse(employees.Text, out int result) == false) 
            {
                message.Text = "Enter the number";
                IsOk= false;
                return;

            }
            if (IsOk == true) 
            {
                departament.Name = name.Text;
                departament.employers = Convert.ToInt32(employees.Text);
                AppDataContext.db.Departament.Add(departament);
                AppDataContext.db.SaveChanges();
                await Navigation.PushAsync(new DepartametPage());
            }

        };
        Content = new VerticalStackLayout { Padding = 24, Spacing = 16, Children = { name, employees, message, save } };
    }
}
