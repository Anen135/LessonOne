using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonOne.models;
using LessonOne.services;

namespace LessonOne.view
{
    public partial class LoginView : ObservableObject
    {
        [ObservableProperty]
        string login = "";
        [ObservableProperty]
        string password = "";
        [ObservableProperty]
        string confirmPassword = "";
        [ObservableProperty]
        string message = "";

        Action<User> onSuccess;

        public LoginView(Action<User> scs)
        {
            onSuccess = scs;
        }


        [RelayCommand]
        void onLogin()
        {
            try
            {
                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
                {
                    throw new Exception("Login or password is null");
                }
                if (Password != ConfirmPassword)
                {
                    throw new Exception("Password not Confirmed");
                }
                var users = DBService.Connection.Table<User>().Where(u => u.Login == Login && u.Password == Password);
                if (users.Count() == 0)
                {
                    throw new Exception("Not Found");
                }
                onSuccess(users.First());

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
