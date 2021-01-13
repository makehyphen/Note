using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Page> Pages { get; set; }
    }
}
