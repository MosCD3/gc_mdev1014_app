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
    public class RegisterPageVM : BaseVM
    {
        public ICommand SignUpCommand => new Command(SignUpClicked);
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordVerif { get; set; }

        private ServerConnect serviceConnect => new ServerConnect();
       

        public RegisterPageVM()
        {
            //IsBusy = true;
        }

        async void SignUpClicked()
        {

            DataCheck();

        }


        async void DataCheck()
        {
            //Checking Email input
            if (!StringOperations.ValidateEmailInput(UserName))
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

            if(!StringOperations.ValidatePasswordInput(Password))
            {
                await MainApp.MainPage.DisplayAlert("Error!", "Password policy mismatch", "ok");
                return;
            }


            if (Password != PasswordVerif)
            {
                await MainApp.MainPage.DisplayAlert("Error!", "Password verify mismatch", "ok");
                return;
            }



            ContinueSignUp();
        }




        async void ContinueSignUp()
        {
            Debug.WriteLine($"am sign up clicked: username:{UserName}, password:{Password}");
            var _user = new UserAuthInfoObject
            {
                Email = UserName,
                Password = Password,
                AuthType = AuthType.SignUp,
            };

            IsBusy = true;

            var result = await serviceConnect.Connect(_user);

            IsBusy = false;

            switch (result)
            {
                case ServerReplyStatus.Fail:
                    MainApp.MainPage.DisplayAlert("Error!", "Something bad has occured", "Ok");
                    break;
                case ServerReplyStatus.UserNameAlreadyUsed:
                    MainApp.MainPage.DisplayAlert("Error!", "Username already exists!", "Ok");
                    break;
                case ServerReplyStatus.PasswordRequirementsFailed:
                    MainApp.MainPage.DisplayAlert("Error!", "Password policy mismatch!", "Ok");
                    break;
                case ServerReplyStatus.Success:
                    MainApp.MainPage.DisplayAlert("Success", "Sign up succeeded!. \nPlease check you email for activating your account before logging in", "Ok");
                    break;
            }
        }
       


    }
}
