using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using firstapp.ENUMS;
using firstapp.Models;
using firstapp.Tools;
using Xamarin.Forms;

namespace firstapp.ViewModels
{
    public class LoginPageVM : BaseVM
    {
        public string PH_Title { get; set; }
        public string Username { get; set; }


        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { SetValue(ref _Password, value); }
        }

        public ICommand SigninCommand => new Command(SignInClicked);

        private ServerConnect serviceConnect => new ServerConnect();
        public LoginPageVM()
        {
            PH_Title = "Login Page";
        }

        async public void SignInClicked()
        {
            CheckData();

        }

        async void CheckData()
        {
            //Checking Email input
            if (!StringOperations.ValidateEmailInput(Username))
            {
                await MainApp.MainPage.DisplayAlert("Error!", "Please enter valid email", "ok");
                return;
            }

            //checking the password
            var result = StringOperations.BasicValidation(Password);
            if (result != null)
            {
                await MainApp.MainPage.DisplayAlert("Error!", result, "ok");
                return;
            }

            ContinueSignIn();
        }

        async void ContinueSignIn()
        {
            Debug.WriteLine($"check against username:{Username}, password:{Password}");
            var _user = new UserAuthInfoObject
            {
                Email = Username,
                Password = Password,
                AuthType = AuthType.SignIn,
            };

            IsBusy = true;
            var result = await serviceConnect.Connect(_user);

            IsBusy = false;

            switch (result)
            {
                case ServerReplyStatus.Success:
                    MainApp.OnLogin();
                    break;
                case ServerReplyStatus.NotConfirmed:
                    await MainApp.MainPage.DisplayAlert("Error!", "Email not confirmed, \nPlease check your email to confirm your account", "Ok");
                    break;
                case ServerReplyStatus.InvalidPassword:
                    await MainApp.MainPage.DisplayAlert("Error!", "Invalid password!", "Ok");
                    break;
                case ServerReplyStatus.UserNotFound:
                    await MainApp.MainPage.DisplayAlert("Error!", "Username not found!", "Ok");
                    break;
                default:
                    await MainApp.MainPage.DisplayAlert("Error!", "Something went wrong", "Ok");
                    break;
            }
           
        }
    }
}
