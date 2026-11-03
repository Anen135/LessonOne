using LessonOne.models;
using LessonOne.view;

namespace LessonOne.pages;

public class UserDetailPage : ContentPage
{
    public UserDetailPage(User user)
    {
        Title = "Edit profile";
        var vm = new UserDetailView(user);
        BindingContext = vm;
        var login = new Entry { Placeholder = "Login" };
        login.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserLogin));
        var password = new Entry { Placeholder = "Password", IsPassword = true };
        password.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserPassword));
        var email = new Entry { Placeholder = "Email", Keyboard = Keyboard.Email };
        email.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserEmail));
        var message = new Label { TextColor = Colors.DarkRed };
        var save = new Button { Text = "Save" };
        save.Clicked += async (_, _) =>
        {
            save.IsEnabled = false;
            try { vm.Save(); await Navigation.PopAsync(); }
            catch (InvalidOperationException ex) { message.Text = ex.Message; }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                message.Text = "Unable to save the profile.";
            }
            finally { save.IsEnabled = true; }
        };
        Content = new VerticalStackLayout { Padding = 24, Spacing = 16, Children = { login, password, email, message, save } };
    }
}
