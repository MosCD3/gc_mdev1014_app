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

            ContinueSignIn(Username,Password);
        }


    }
}
