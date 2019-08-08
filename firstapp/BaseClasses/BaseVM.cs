using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using firstapp.ENUMS;
using firstapp.Models;
using Xamarin.Forms;

namespace firstapp
{
    public class BaseVM : INotifyPropertyChanged
    {
        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetValue(ref _IsBusy, value); }
        }

        protected App MainApp = Application.Current as App;

        public BaseVM()
        {
        }

        private async void TestAsync()
        {
            await Task.Delay(10000);
            Debug.WriteLine("got response from server");
            IsBusy = false;
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


        async public void ContinueSignIn(string _username, string _password)
        {
            Debug.WriteLine($"check against username:{_username}, password:{_password}");
            var _user = new UserAuthInfoObject
            {
                Email = _username,
                Password = _password,
                AuthType = AuthType.SignIn,
            };
            ServerConnect serviceConnect = new ServerConnect();


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
