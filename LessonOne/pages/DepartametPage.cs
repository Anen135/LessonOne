using LessonOne.models;
using LessonOne.services;
using Microsoft.Maui.Controls.Shapes;

namespace LessonOne.pages;

public class DepartametPage : ContentPage
{
    private readonly DepartametView vm = new();
    private readonly Label message = new() { TextColor = Colors.DarkRed };

    public DepartametPage()
    {
        Title = "Departments";
        BackgroundColor = Color.FromArgb("#F2F5FC");
        vm.Load();
        Button new_department = new() 
        {
            Text = "Add department"
        };
        new_department.Clicked += async (_, _) => await Navigation.PushAsync(new DepartamentCreatePage());
        Label title = new()
        {
            Text = "Our Departaments",
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        };
        Label subtitle = new()
        {
            Text = "Manage your departaments",
            FontSize=15,
            TextColor = Color.FromArgb("#DCE7FF")
        };
        Border header = new()
        {
            BackgroundColor = Color.FromArgb("#3867F5"),
            Stroke = Colors.Transparent,
            StrokeShape = new RoundRectangle { CornerRadius = 20 },
            Padding = 25,
            Content = new VerticalStackLayout
            {
                Spacing = 8,
                Children = { title, subtitle }
            }
        };
        Label listTitle = new()
        {
            Text = "Departament list",
            FontSize = 21,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#252B3A")
        };
        var items = new CollectionView
        {
            ItemsSource = vm.Departaments,
            EmptyView = new Label
            {
                Text = "No dempartaments found",
                TextColor = Colors.Gray,
                FontSize = 16,
                HorizontalTextAlignment = TextAlignment.Center
            },
            ItemTemplate = new DataTemplate(() =>
            {
                var name = new Label { VerticalOptions = LayoutOptions.Center };
                name.SetBinding(Label.TextProperty, nameof(Departament.Name));
                var edit = new Button { Text = "Edit" };
                var delete = new Button { Text = "Delete", BackgroundColor = Color.FromArgb("FDECEC"), TextColor = Color.FromArgb("D93636"),
                CornerRadius = 9, FontSize = 13, HeightRequest = 40};
                edit.Clicked += async (sender, _) =>
                {
                    if (sender is Button { BindingContext: Departament department })
                        await Navigation.PushAsync(new DepartamentDetailPage(department));
                };
                delete.Clicked += async (sender, _) =>
                {
                    if(sender is Button { BindingContext: Departament department })
                    {
                        bool result = await DisplayAlert("Delete departament", $"Are you sure ypu want to delete {department.Name}", "Yes", "No");
                        if (result)
                        {
                            try
                            {
                                AppDataContext.db.Remove(department);
                                AppDataContext.db.SaveChanges();
                                vm.Departaments.Remove(department);
                            }
                            catch (Exception ex)
                            {
                                await DisplayAlert("Error", ex.Message, "Ok");
                            }
                        }
                    }
                };
                Border card = new()
                {
                    BackgroundColor = Colors.White,
                    Stroke = Color.FromArgb("#E8ECF5"),
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = 15
                    },
                    Padding = 15,
                    Content = new VerticalStackLayout { Spacing = 12, Children = {name, new HorizontalStackLayout { Spacing = 10, Children = {edit, delete}} }}
                };
                return card;
            }), 
            SelectionMode = SelectionMode.None
        };
        message.FontSize = 13;
        message.TextColor = Colors.DarkRed;
        Content = new ScrollView
        {
            Content = new VerticalStackLayout { Padding = 22, Spacing = 22, Children = { header, listTitle, items, new_department, message} }
        };
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
