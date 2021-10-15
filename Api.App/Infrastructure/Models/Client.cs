using Api.App.Core;
using Api.Infrastructure.MySql;
using System;
using System.Net.Mail;

namespace Api.App.Infrastructure.Models
{
    public class Client
    {
        public uint Id_klienta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Numer_telefonu { get; set; }
        public string Adres_email { get; set; }

        public bool Valide(out string message)
        {
            message = string.Empty;
            if (!Helpers.LengthBetween(Imie, 1, 30))
            {
                message = "Name must be between 1-30 chars";
                return false;
            }
            if (!Helpers.LengthBetween(Nazwisko, 1, 30))
            {
                message = "Lastname must be between 1-30 chars";
                return false;
            }
            if (!Helpers.PhoneNumber(Numer_telefonu))
            {
                message = "Value must be phone number format";
                return false;
            }
            if (!Helpers.EmailValide(Adres_email) && Adres_email != "")
            {
                message = "Value must be email format or empty";
                return false;
            }


            return true;
        }

        public bool ValideUpdate(out string message)
        {
            message = string.Empty;
            if (!Helpers.LengthBetween(Imie, 1, 30) && Imie != "")
            {
                message = "Name must be between 1-30 chars";
                return false;
            }
            if (!Helpers.LengthBetween(Nazwisko, 1, 30) && Nazwisko !="")
            {
                message = "Lastname must be between 1-30 chars";
                return false;
            }
            if (!Helpers.PhoneNumber(Numer_telefonu) && Numer_telefonu !="")
            {
                message = "Value must be phone number format";
                return false;
            }
            if (!Helpers.EmailValide(Adres_email) && Adres_email !="")
            {
                message = "Value must be email format or empty";
                return false;
            }


            return true;
        }
    }
}
