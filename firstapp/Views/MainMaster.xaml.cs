using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace firstapp
{
    public partial class MainMaster : ContentPage
    {
        MasterDetailPage myMaster;
        public MainMaster(MasterDetailPage _myMaster)
        {
            myMaster = _myMaster;
            InitializeComponent();
        }

        void Products_Handle_Clicked(object sender, System.EventArgs e)
        {
            myMaster.Detail = new NavigationPage(new ProductsPage());
            myMaster.IsPresented = false;
            
        }
        void Settings_Handle_Clicked(object sender, System.EventArgs e)
        {
            myMaster.Detail = new NavigationPage(new SettingsPage());
            myMaster.IsPresented = false;

        }
        void Inv_Handle_Clicked(object sender, System.EventArgs e)
        {
            myMaster.Detail = new NavigationPage(new PetsPage());
            myMaster.IsPresented = false;

        }

        void AddPets_Handle_Clicked(object sender, System.EventArgs e)
        {
            myMaster.Detail = new NavigationPage(new AddPet());
            myMaster.IsPresented = false;
        }
    }
}
