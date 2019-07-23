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
    public class LoginPageVM : INotifyPropertyChanged
    {
        public string PH_Title { get; set; }
        public string Username { get; set; }


        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { SetValue(ref _Password, value); }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetValue(ref _IsBusy, value); }
        }




        public ICommand SigninCommand => new Command(SignInClicked);

        private ServerConnect serviceConnect => new ServerConnect();
        public LoginPageVM()
        {
            PH_Title = "Login Page";
            IsBusy = true;
            TestAsync();
        }

        //Start Inotify from here
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;
            backingField = value;
            OnPropertyChanged(propertyName);
        }
        //End Inotify from here

        private async void TestAsync()
        {
            await Task.Delay(10000);
            Debug.WriteLine("got response from server");
            Password = "IamAcompliatedPass";
            IsBusy = false;
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

            var result = await serviceConnect.Connect(_user);

        }
    }
}
