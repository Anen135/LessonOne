using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonOne.models;
using LessonOne.services;
using Microsoft.EntityFrameworkCore;

namespace LessonOne.view
{
    public partial class SecondView : ObservableObject
    {
        [ObservableProperty]
        string login = "";
        private string? Login;
        [ObservableProperty]
        string password = "";
        private string? Password;
        [ObservableProperty]
        string confirmPassword = "";
        private string ConfirmPassword;
        [ObservableProperty]
        string message = "";

        Action<User> onSuccess;
        
        public SecondView(Action<User> scs)
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
                    throw new Exception();
                }
                if (Password != ConfirmPassword)
                {
                    var user = AppDataContext.db.Users.FirstOrDefault(u => u.Login == Login && u.Password == Password);

                    onSuccess(user);
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }
    }
}
