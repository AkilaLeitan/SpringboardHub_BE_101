using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestAddCourse
    {
        public string CourseName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Duration { get; set; } = string.Empty;
    }
}