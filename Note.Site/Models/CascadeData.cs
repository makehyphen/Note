using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Models
{
    public class CascadeData
    {
        public User User { get; set; }
        public Settings Settings { get; set; }
        public List<Book> Books { get; set; }
        public History History { get; set; }

        public Book SelectedBook => Books.SingleOrDefault(x => x.Id == History.SelectedBookId);
        public Page SelectedPage => SelectedBook.Pages.SingleOrDefault(x => x.Id == History.SelectedPageId);

        public ComponentBase DataComponentBase { get; set; }


    }
}
