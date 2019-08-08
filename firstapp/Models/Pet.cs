using System;
namespace firstapp.Models
{
    public class Pet
    {
        public string PetID;
        public string UserID;
        public string PetName { get; set; }
        public string PetBreed { get; set; }
        public string PetDesc { get; set; }
    }
}