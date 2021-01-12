using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Inner { get; set; }
        public bool Saved { get; set; }
    }
}
