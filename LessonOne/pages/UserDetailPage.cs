using LessonOne.models;
using LessonOne.view;
using Microsoft.Maui.Controls.Shapes;

namespace LessonOne.pages;

public class UserDetailPage : ContentPage
{
    public UserDetailPage(User user)
    {
        Title = "Edit profile";
        BackgroundColor = Color.FromArgb("#101D35");
        var vm = new UserDetailView(user);
        BindingContext = vm;
        var title = new Label
        {
            Text = "profile settings",
            FontSize = 13,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#42B9FF"),
            CharacterSpacing = 2
        };
        var profileTitle = new Label
        {
            Text = "my profile",
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        };
        var subtitle = new Label
        {
            Text = "personal information and account settings",
            FontSize = 13, 
            TextColor = Color.FromArgb("#A9B9D0")
        };
        var userInfo = new VerticalStackLayout
        {
            Spacing = 8,
            HorizontalOptions = LayoutOptions.Center, 
            Children = {new Label { Text = user.Login, FontSize = 19, FontAttributes = FontAttributes.Bold, TextColor = Colors.White, HorizontalTextAlignment = TextAlignment.Center }, 
            new Label{Text = "account information", FontSize = 12, TextColor = Color.FromArgb("#A9B9D0"), HorizontalTextAlignment = TextAlignment.Center}}
        };
        Label CreateCaption(string text)
        {
            return new Label
            {
                Text = text.ToUpper(),
                FontSize = 11,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#42B9FF"),
                CharacterSpacing = 1
            };
        }
        var login = new Entry
        {   Placeholder = "Login",
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#879BB8"),
            BackgroundColor = Color.FromArgb("#263650")
        };
        login.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserLogin));
        var password = new Entry 
        {   Placeholder = "Password",
            IsPassword = true,
            TextColor =Colors.White,
            PlaceholderColor = Color.FromArgb("879BB8"),
            BackgroundColor = Color.FromArgb("#263650")
        };
        password.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserPassword));
        var email = new Entry 
        {
            Placeholder = "Email", 
            Keyboard = Keyboard.Email,
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("879BB8"),
            BackgroundColor = Color.FromArgb("#263650")
        };
        email.SetBinding(Entry.TextProperty, nameof(UserDetailView.UserEmail));
        var accessValue = new Label
        {
            Text = user.Access.ToString(),
            FontSize = 17,
            FontAttributes = FontAttributes.Bold,
            TextColor= Color.FromArgb("#42B9FF"), 
            VerticalOptions = LayoutOptions.Center
        };
        var accessCard = new Border
        {
            BackgroundColor = Color.FromArgb("#263650"),
            Stroke = Color.FromArgb("#385273"),
            StrokeShape = new RoundRectangle { CornerRadius = 12},
            Padding = 15,
            Content = new VerticalStackLayout { Spacing = 7, Children = {CreateCaption("Access Level"), accessValue}}
        };
        Border createField(string caption, Entry entry)
        {
            return new Border
            {
                BackgroundColor = Color.FromArgb("#263650"),
                Stroke = Color.FromArgb("#385273"),
                StrokeShape = new RoundRectangle { CornerRadius = 12 },
                Padding = 12,
                Content = new VerticalStackLayout { Spacing = 5, Children = { CreateCaption(caption), entry } }
            };
        }
        var message = new Label 
        { 
            TextColor = Color.FromArgb("#FF7777"),
            FontSize = 13
        };
        var save = new Button 
        { 
            Text = "save changes",
            BackgroundColor = Color.FromArgb("#1689E8"),
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            FontSize = 17,
            CornerRadius = 12,
            HeightRequest = 52
        };
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
        var profileCard = new Border
        {
            BackgroundColor = Color.FromArgb("#1B2B46"),
            Stroke = Color.FromArgb("#304866"),
            StrokeShape = new RoundRectangle { CornerRadius = 20 },
            Padding = 20,
            Content = new VerticalStackLayout { Spacing = 20, Children = { userInfo, 
                    new BoxView 
                    { 
                        HeightRequest = 1,
                        Color =Color.FromArgb("#385273"),
                    }, createField("login", login), createField("password", password), createField("email", email), accessCard, message, save} }
        };
        Content = new ScrollView { Content = new VerticalStackLayout { Padding = new Thickness(22,25), Spacing = 20, Children = { title, profileTitle, subtitle, profileCard } } };
    }
}
