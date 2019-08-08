using System;
using System.Diagnostics;
using System.Threading.Tasks;
using firstapp.ENUMS;
using firstapp.Models;
using firstapp.ViewModels;
using Xamarin.Forms;

namespace firstapp
{
    public class BasePage : ContentPage
    {
        public App MyApp = Application.Current as App;
        public BasePage()
        {
            SetbindingContext();
        }

        private bool CheckSession()
        {
            SessionStatus is_logged = MyApp.Session.IsLoggedIn();
            return is_logged == SessionStatus.LoggedInWithActiveSession;
        }

        protected override void OnAppearing()
        {
            if (this.GetType() != typeof(MainPage)
               && this.GetType() != typeof(MainMaster)
               && this.GetType() != typeof(LoginPage)
               && this.GetType() != typeof(RegisterPage)
               )
            {

                var response = CheckSession();
                if (!response)
                {
                    RefreshSession();
                    return;
                }
            }

        }

        private async Task RefreshSession()
        {
            var response = await MyApp.MainPage.DisplayAlert("Attention!", "Your session is exprired!. Please choose what do you want to do", "Sign Out", "Refresh Session");
            if (response)
                MyApp.OnLogout();
            else DoRefreshSession();

        }


        private void DoRefreshSession()
        {
            Debug.WriteLine("refreshing session");
            var _myBinding = BindingContext as BaseVM;
            _myBinding.ContinueSignIn(MyApp.Session.UserName, MyApp.Session.UserPassword);
        }

        private void SetbindingContext()
        {
            if (this.GetType() == typeof(MainPage))
                BindingContext = new MainPageVM();
            else if (this.GetType() == typeof(LoginPage))
                BindingContext = new LoginPageVM();
            else if (this.GetType() == typeof(RegisterPage))
                BindingContext = new RegisterPageVM();
            else if (this.GetType() == typeof(AddPet))
                BindingContext = new AddPetVM();
            else if (this.GetType() == typeof(PetsPage))
                BindingContext = new PetsPageVM();
            else if (this.GetType() == typeof(MainDetails))
                BindingContext = new MainDetailsVM();

        }
    }
}
