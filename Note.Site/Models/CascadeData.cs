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

        public Action Callback { get; set; }

        public bool SavingEnabled { get; set; }

        private Book selectedBook;
        public Book SelectedBook
        {
            get
            {
                if (History.SelectedBookId == null || History.SelectedBookId == default)
                {
                    return default;
                }
                else
                {
                    selectedBook = Books.ToList().SingleOrDefault(x => x.Id == History.SelectedBookId);
                    return selectedBook;
                }
            }
            set
            { selectedBook = value; }
        }
        public Page SelectedPage
        {
            get
            {
                if (SelectedBook == null)
                {
                    return default;
                }
                else
                {
                    return SelectedBook.Pages.SingleOrDefault(x => x.Id == History.SelectedPageId);
                }
            }
            set => SelectedPage = value;
        }


        public ComponentBase DataComponentBase { get; set; }


    }
}
