using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Request
{
    public class RequestAddSubject
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credit { get; set; } = 0;
        public string? TextBook { get; set; }
    }
}