using System;
using System.Diagnostics;
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

        private void SetbindingContext()
        {
            if (this.GetType() == typeof(MainPage))
            {
                //BindingContext = Activator.CreateInstance(typeof(MainPageVM));
                BindingContext = new MainPageVM();
            }
            else if(this.GetType() == typeof(LoginPage))
            {
                BindingContext = new LoginPageVM();
            }
            else if(this.GetType() == typeof(RegisterPage))
            {
                BindingContext = new RegisterPageVM();
            }
            else if (this.GetType() == typeof(AddPet))
            {
                BindingContext = new AddPetVM();
            }

        }
    }
}
