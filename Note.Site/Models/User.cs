using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Models
{
    public class User
    {
        public string Username { get; set; }
        public string ImageUrl { get; set; }
        public bool LoggedIn { get; set; }
    }
}
