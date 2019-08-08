using System;
using System.Text.RegularExpressions;

namespace firstapp
{
    public static class Keys
    {
        public static string AWS_PoolID = "ca-central-1_QcjavvJaM";
        public static string AWS_UsersPool_ClientID = "onfk8599oc901ahnducsec1qq";
        //Regex for input validation
        public static Regex Regex_EntryName = new Regex("^[a-zA-Z0-9_ ]*$"); // For Names allow alphabets, numbers, space and '_'
        public static Regex Regex_EntryIP = new Regex(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\.([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\.([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\.([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$"); // For IP address
        public static string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:
        public static string passwordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

        //AWS related keys
        public static string Aws_Resource_Folder = "https://qdgbfsrabc.execute-api.ca-central-1.amazonaws.com/dev";
        public static string Aws_Resource_SavePet = "/pet-save";
        public static string Aws_Resource_PetsLoad = "/pets-load";
    }
}
