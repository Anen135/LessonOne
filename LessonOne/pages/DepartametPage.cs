using LessonOne.models;
using LessonOne.services;

namespace LessonOne.pages;

public class DepartametPage : ContentPage
{
    private readonly DepartametView vm = new();
    private readonly Label message = new() { TextColor = Colors.DarkRed };

    public DepartametPage()
    {
        Title = "Departments";
        var items = new CollectionView
        {
            ItemsSource = vm.Departaments,
            EmptyView = "No departments found.",
            ItemTemplate = new DataTemplate(() =>
            {
                var name = new Label { VerticalOptions = LayoutOptions.Center };
                name.SetBinding(Label.TextProperty, nameof(Departament.Name));
                var edit = new Button { Text = "Edit" };
                edit.Clicked += async (sender, _) =>
                {
                    if (sender is Button { BindingContext: Departament department })
                        await Navigation.PushAsync(new DepartamentDetailPage(department));
                };
                return new HorizontalStackLayout { Spacing = 16, Children = { name, edit } };
            })
        };
        var grid = new Grid { Padding = 24, RowSpacing = 16, RowDefinitions = { new RowDefinition(GridLength.Auto), new RowDefinition(GridLength.Star) } };
        grid.Add(message, 0, 0);
        grid.Add(items, 0, 1);
        Content = grid;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try { vm.Load(); message.Text = string.Empty; }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            message.Text = "Unable to load departments.";
        }
    }
}
