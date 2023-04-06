using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringboardHub_BE_101.DTO.Response
{
    public class ResponseArticleDetails
    {
        public int ArticleID { get; set; } = AppConstants.DEFAULT;
        public string UID { get; set; } = AppConstants.DEFAULT_ID;
        public int TitleID { get; set; } = AppConstants.DEFAULT;
        public int LectureID { get; set; } = AppConstants.DEFAULT;
        public string Topic { get; set; } = string.Empty;
        public string? Description { get; set; }
        public VisibleTypes visibaleType { get; set; } = VisibleTypes.defaultType;
        public bool isUpdated { get; set; } = AppConstants.DEFAULT_BOOLEAN;
    }
}