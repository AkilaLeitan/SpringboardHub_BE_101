using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.Model
{
    public class Title : DbBaseClass
    {
        public int TitleID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public string Name { get; set; } = string.Empty;
        public Subject? Subject { get; set; }
        public int SubjectID { get; set; }= AppConstants.DEFAULT;
        public ICollection<Article>? Articles { get; set; }

    }
}