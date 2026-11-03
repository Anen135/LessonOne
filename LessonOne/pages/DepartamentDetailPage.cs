using LessonOne.models;
using LessonOne.services;

namespace LessonOne.pages;

public class DepartamentDetailPage : ContentPage
{
    public DepartamentDetailPage(Departament department)
    {
        Title = "Edit department";
        var name = new Entry { Text = department.Name, Placeholder = "Name" };
        var message = new Label { TextColor = Colors.DarkRed };
        var save = new Button { Text = "Save" };
        save.Clicked += async (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                message.Text = "Enter a department name.";
                return;
            }
            save.IsEnabled = false;
            var previousName = department.Name;
            try
            {
                department.Name = name.Text.Trim();
                AppDataContext.db.SaveChanges();
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                department.Name = previousName;
                System.Diagnostics.Debug.WriteLine(ex);
                message.Text = "Unable to save the department.";
            }
            finally { save.IsEnabled = true; }
        };
        Content = new VerticalStackLayout { Padding = 24, Spacing = 16, Children = { name, message, save } };
    }
}
