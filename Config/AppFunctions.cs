using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Config
{
    public class AppFunctions
    {
        public static bool EmailValidater(string email)
        {

            var result = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static bool TelephoneValidater(string phoneNumber)
        {
            Regex regex = new Regex(@"^0\d{9}$");
            return regex.IsMatch(phoneNumber);
        }

        public static bool StringNullOrEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }
    }

    public class NoParam
    {

    }

}