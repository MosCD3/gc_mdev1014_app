using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace firstapp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            if (Properties.ContainsKey("Username"))
                OnLogin();
            else
                MainPage = new NavigationPage(new MainPage());
        }

        public void OnLogin()
        {
            MainPage = new AfterLoginPage();

        }
        async public void OnLogout()
        {
            Properties.Remove("Username");
            Properties.Remove("Password");
            await SavePropertiesAsync();

            MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        public void SetSessionData(SignInContext _sessionData)
        {

            Properties["Username"] = _sessionData.UserName;
            Properties["Password"] = _sessionData.UserPassword;
            SavePropertiesAsync();
        }
    }
}
