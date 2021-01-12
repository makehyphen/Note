using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Models
{
    public class History
    {
        public Guid SelectedBookId { get; set; }
        public Guid SelectedPageId { get; set; }
    }
}
