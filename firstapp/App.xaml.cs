using System;
using System.Threading.Tasks;
using firstapp.ENUMS;
using firstapp.Models;
using firstapp.Tools;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace firstapp
{
    public partial class App : Application
    {
        public PropertiesManager AppP;
        public SessionStore Session;
        public Pet[] Pets;
        public bool isPetsLoaded;

        public App()
        {
            AppP = new PropertiesManager();
            Session = new SessionStore();

            InitializeComponent();

            //MainPage = new MainPage();
            if (Properties.ContainsKey("Username"))
                OnLogin();
            else
                MainPage = new NavigationPage(new MainPage());



            SessionStatus is_logged = Session.IsLoggedIn();

            switch (is_logged)
            {
                case SessionStatus.LoggedOut:
                    NavigateMainPage(Pages.Landing);
                    break;
                case SessionStatus.LoggedInWithActiveSession:
                    NavigateMainPage(Pages.AfterLogin);
                    break;
                case SessionStatus.LoggedInWithExpiredSession:
                    NavigateMainPage(Pages.AfterLogin);
                    break;
                default:
                    NavigateMainPage(Pages.Landing);
                    break;

            }
        }

        public Task NavigateMainPage(Pages key)
        {

            switch (key)
            {
                case Pages.Landing:
                    MainPage = new NavigationPage(new MainPage());

                    break;
                case Pages.AfterLogin:
                    MainPage = new AfterLoginPage();
                    break;
                default:
                    MainPage = new NavigationPage(new MainPage());
                    break;

            }
            return Task.FromResult(true);
        }


        public void OnLogin()
        {
            NavigateMainPage(Pages.AfterLogin);

        }


        async public void OnLogout()
        {
            Properties.Remove("Username");
            Properties.Remove("Password");
            await SavePropertiesAsync();
            NavigateMainPage(Pages.Landing);
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


    
    }
}
