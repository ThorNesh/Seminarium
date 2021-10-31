using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Core
{
    public static class Helpers
    {
        public static bool EmailValide(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool PhoneNumber(string number)
        {
            return (uint.TryParse(number, out uint result) && number.Length == 9);
        }
        public static bool LengthBetween(string var, int min, int max)
        {
            return var != null ? var.Length > min && var.Length < max : false;
        }

        public static string FloatFormater(float var)
        {
            return var.ToString().Replace(',', '.');
        }

        public static bool PeselValidate(string pesel)
        {
            return pesel.Length == 11 && uint.TryParse(pesel, out _);
        }
        public static string ConvertDate(DateTime time)
        {
            return $"{time.Year}-{time.Month}-{time.Day}";
        }
    }
}
