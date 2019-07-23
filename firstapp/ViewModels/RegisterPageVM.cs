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
    public class RegisterPageVM : BaseVM
    {
        public ICommand SignUpCommand => new Command(SignUpClicked);
        public string UserName { get; set; }
        public string Password { get; set; }

        private ServerConnect serviceConnect => new ServerConnect();

        public RegisterPageVM()
        {
            IsBusy = true;
        }

        async void SignUpClicked()
        {
            Debug.WriteLine($"am sign up clicked: username:{UserName}, password:{Password}");
            var _user = new UserAuthInfoObject
            {
                Email = UserName,
                Password = Password,
                AuthType = AuthType.SignUp,
            };

            var result = await serviceConnect.Connect(_user);


        }


    }
}
