using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Config
{
    public class AppConstants
    {
        public static string DEFAULT_ID = "default_id";
        public static int DEFAULT = 0;
        public static bool DEFAULT_BOOLEAN = false;
        public static string DEFAULT_RESPONSE_CODE_SUCCSSES = "00";
        public static string DEFAULT_RESPONSE_CODE_UNAUTHORIZED = "01";
        public static string DEFAULT_RESPONSE_CODE_BADREQUEST = "02";
        public static string DEFAULT_RESPONSE_CODE_NOTFOUND = "03";
        public static string DEFAULT_RESPONSE_CODE_SERVERSIDE_ERROR = "05";
        public static string DEFAULT_RESPONSE_MESSAGE_SUCCSSES = "Success";
        public static string DEFAULT_RESPONSE_MESSAGE_NOTFOUND = "Your Details Not Found";
        public static string DEFAULT_RESPONSE_MESSAGE_UNAUTHORIZED = "You don't have permission for perform this task";
        public static string DEFAULT_RESPONSE_MESSAGE_LOGINFAILED = "Your username or password is worng";
    }
}