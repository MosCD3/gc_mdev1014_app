using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using firstapp.ENUMS;
using firstapp.Models;
using Newtonsoft.Json;

namespace firstapp.ViewModels
{
    public class PetsPageVM : BaseVM
    {
        public string _msg;
        public string msg
        {
            get { return _msg; }
            set { SetValue(ref _msg, value); }
        }

        public ObservableCollection<Pet> _itemslist = new ObservableCollection<Pet>();
        public ObservableCollection<Pet> ItemsList
        {
            get { return _itemslist; }
            set { SetValue(ref _itemslist, value); }
        }


        public PetsPageVM()
        {
            msg = "still loading";
            LoadItems(false);
        }

        public void LoadItems(bool fromServer)
        {

            if (fromServer)
                LoadItemsServer();
            else
            {
                if (MainApp.Pets != null)
                    ItemsList = new ObservableCollection<Pet>(MainApp.Pets);
                else
                    LoadItemsServer();
            }

        }

        private async void LoadItemsServer()
        {
            var _object = new GenericID_Action
            {
                UserID = MainApp.Session.UserID,// ID will be extracted from Auth pool in aws
            };

            var serverConnect = new ServerConnect();


            IsBusy = true;
            var response = await serverConnect.ConnectApi(_object,Keys.Aws_Resource_PetsLoad);
            IsBusy = false;

            if (response.status != ServerReplyStatus.Success)
            {
                await MainApp.MainPage.DisplayAlert("Error!", "Error loading pets data!", "Ok");
                return;
            }

            Debug.WriteLine("list data are:");
            Debug.WriteLine(response.data);

            var result = JsonConvert.DeserializeObject<BaseReturnPets>(response.data);
            ItemsList = new ObservableCollection<Pet>(result.Pets);

        }
    }
}
