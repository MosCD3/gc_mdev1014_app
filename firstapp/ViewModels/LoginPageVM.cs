using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using firstapp.ENUMS;
using firstapp.Models;
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



        private App MainApp = Application.Current as App;


        public ICommand SigninCommand => new Command(SignInClicked);

        private ServerConnect serviceConnect => new ServerConnect();
        public LoginPageVM()
        {
            PH_Title = "Login Page";
        }

        async public void SignInClicked()
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

            if (!(result == ServerReplyStatus.Success))
            {
                await MainApp.MainPage.DisplayAlert("Error!", "Something went wrong", "Ok");
            }
            else MainApp.OnLogin();

        }
    }
}
