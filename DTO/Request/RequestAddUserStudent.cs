using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestAddUserStudent
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NIC { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Guardian { get; set; } = string.Empty;
        public string JoinedYear { get; set; } = AppConstants.DEFAULT_ID;
        public DateTime DOB { get; set; } = DateTime.MinValue;
        public int BatchID { get; set; } = AppConstants.DEFAULT;
    }
}