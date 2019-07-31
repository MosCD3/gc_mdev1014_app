using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using firstapp.Models;
using firstapp.ViewModels;
using Xamarin.Forms;

namespace firstapp
{
    public partial class MainPage : BasePage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var newPage = new NavigationPage(new LoginPage());
            Navigation.PushAsync(newPage);
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
        async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            var serverConnect = new ServerConnect();
            var petObject = new Pet
            {
                PetID = "234234234hsjdfhsdf",
                PetName = "NaaamX",
                PetBreed = "Boxer555",
                PetDesc = "From xamarin data"
            };

            var response = await serverConnect.ConnectApi(petObject);

            Debug.WriteLine($"the operation response was: {response}");
        }
    }
}
