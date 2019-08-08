using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace firstapp
{
    public partial class SettingsPage : BasePage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            MyApp.OnLogout();
        }
    }
}
