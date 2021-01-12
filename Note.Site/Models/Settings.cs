using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Models
{
    public class Settings
    {
        public bool IsDarkModeEnabled { get; set; }
        public bool IsMarkdownPreviewEnabled { get; set; }
        public bool IsScrollAlligmentEnabled { get; set; }
        public bool IsSidebarCollapsed { get; set; }
    }
}
