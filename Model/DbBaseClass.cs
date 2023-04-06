using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class DbBaseClass
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }
    }
}