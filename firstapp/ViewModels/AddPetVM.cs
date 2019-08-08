using System;
using System.Diagnostics;
using System.Windows.Input;
using firstapp.ENUMS;
using firstapp.Models;
using firstapp.Tools;
using Xamarin.Forms;

namespace firstapp.ViewModels
{
    public class AddPetVM : BaseVM
    {
        public string Pet_Name { get; set; }
        public string Pet_Breed { get; set; }
        public string Pet_Desc { get; set; }

        public ICommand AddPetCommand => new Command(AddPetAction);

        public AddPetVM()
        {
        }


        async private void AddPetAction()
        {
            var serverConnect = new ServerConnect();
            var petObject = new Pet
            {
                PetID = StringOperations.GenerateID(),
                UserID = MainApp.Session.UserID,
                PetName = Pet_Name,
                PetBreed = Pet_Breed,
                PetDesc = Pet_Desc
            };

            IsBusy = true;
            ServerResponseObject response = await serverConnect.ConnectApi(petObject, Keys.Aws_Resource_SavePet);
            IsBusy = false;

            if (response.status == ServerReplyStatus.Success)
                await MainApp.MainPage.DisplayAlert("Attention!", "Pet data saved successfully!", "Ok");
            else if(response.status == ServerReplyStatus.Fail)
                await MainApp.MainPage.DisplayAlert("Attention!", "Error saving pet data!", "Ok");
            else if(response.status == ServerReplyStatus.Unknown)
                await MainApp.MainPage.DisplayAlert("Attention!", $"Error saving pet data!:{response.error}", "Ok");
        }
    }
}