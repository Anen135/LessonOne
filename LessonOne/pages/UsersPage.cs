using LessonOne.models;
using LessonOne.services;
using LessonOne.view;

namespace LessonOne.pages;

public class UsersPage : ContentPage
{
    private readonly UserView vm = new();
    private readonly Label message = new() { TextColor = Colors.DarkRed };
    private readonly Label signedInLabel = new();

    public UsersPage()
    {
        Title = "Users";
        BindingContext = vm;
        Content = Build();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (AppDataContext.CurrentUser is null)
        {
            vm.Users.Clear();
            await Shell.Current.GoToAsync("//login");
            return;
        }

        signedInLabel.Text = $"Signed in as {AppDataContext.CurrentUser.Login}";
        message.Text = string.Empty;
        try
        {
            vm.Load();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            message.Text = "Unable to load users. Open this page again to retry.";
        }
    }

    private View Build()
    {
        var users = new CollectionView
        {
            ItemsSource = vm.Users,
            EmptyView = "No users found.",
            SelectionMode = SelectionMode.None,
            ItemTemplate = new DataTemplate(() => BuildUserView())
        };
        var logoutButton = new Button { Text = "Log out", AutomationId = "LogoutButton" };
        logoutButton.Clicked += async (_, _) =>
        {
            logoutButton.IsEnabled = false;
            try
            {
                AppDataContext.CurrentUser = null;
                vm.Users.Clear();
                signedInLabel.Text = string.Empty;
                await Shell.Current.GoToAsync("//login");
            }
            finally
            {
                logoutButton.IsEnabled = true;
            }
        };

        var layout = new Grid
        {
            Padding = 24,
            RowSpacing = 16,
            RowDefinitions =
            {
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Auto)
            }
        };
        layout.Add(signedInLabel, 0, 0);
        layout.Add(message, 0, 1);
        layout.Add(users, 0, 2);
        layout.Add(logoutButton, 0, 3);
        return layout;
    }

    private object BuildUserView()
    {
        var loginLabel = new Label { FontAttributes = FontAttributes.Bold, FontSize = 18 };
        loginLabel.SetBinding(Label.TextProperty, nameof(User.Login));
        var emailLabel = new Label();
        emailLabel.SetBinding(Label.TextProperty, nameof(User.Email));
        var accessLabel = new Label { FontSize = 13 };
        accessLabel.SetBinding(Label.TextProperty, new Binding(nameof(User.Access), stringFormat: "Access: {0}"));
        Button btn = new();
        btn.Text = "change some information";
        btn.Clicked += (s, _) =>
        {
            User usr = null;
            Button _b = s as Button;
            usr = _b.BindingContext as User;
            Navigation.PushAsync(new UserDetailPage(usr));
        };
        return new Border
        {
            Padding = 16,
            Margin = new Thickness(0, 0, 0, 10),
            Content = new VerticalStackLayout
            {
                Spacing = 6,
                Children = { loginLabel, emailLabel, accessLabel, btn }
            }
        };
    }
}
