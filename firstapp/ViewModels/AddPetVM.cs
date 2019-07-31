using System;
using System.Windows.Input;
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


        private void AddPetAction()
        {

        }
    }
}

 //<Entry Placeholder = "Pet Name" Text="{Binding Pet_Name}"/>
            //<Entry Placeholder = "Pet Breed" Text="{Binding Pet_Breed}"/>
            //<Entry Placeholder = "Pet Description" Text="{Binding Pet_Desc}"/>