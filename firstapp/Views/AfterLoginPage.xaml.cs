using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace firstapp
{
    public partial class AfterLoginPage : MasterDetailPage
    {
        public AfterLoginPage()
        {
            var masterPage = new MainMaster(this);
            Master = masterPage;
            Detail = new NavigationPage(new MainDetails());

            InitializeComponent();
        }
    }
}
