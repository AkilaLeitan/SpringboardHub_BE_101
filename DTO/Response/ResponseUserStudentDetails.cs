using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Response
{
    public class ResponseUserStudentDetails
    {
        public int StudentID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = string.Empty;
        public string BatchUID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string NIC { get; set; } = string.Empty;
        public string Guardian { get; set; } = string.Empty;
        public string JoinedYear { get; set; } = AppConstants.DEFAULT_ID;
        public DateTime DOB { get; set; } = DateTime.MinValue;
    }
}