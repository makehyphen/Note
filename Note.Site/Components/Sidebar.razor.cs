using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Note.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class SidebarBase : ComponentBase
    {
        [CascadingParameter]
        public CascadeData Data { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public void UpdateCollapsed()
        {
            Data.Settings.IsSidebarCollapsed = !Data.Settings.IsSidebarCollapsed;
            //StateHasChanged();
        }

        #region Search

        public List<SearchResult> SearchResultList { get; set; } = null;

        public class SearchResult
        {
            public Guid BookId { get; set; }
            public Guid PageId { get; set; }
            public string PageTitle { get; set; }
            public string StringContaining { get; set; }
        }

        public bool SearchValueEnabled { get; set; }

        public async Task ToggleSearch()
        {
            SearchValueEnabled = !SearchValueEnabled;
        }

        public async Task ClearInput()
        {
            SearchValue = string.Empty;
        }

        private string searchValue;

        public string SearchValue
        {
            get
            {
                return searchValue ?? string.Empty;
            }

            set
            {
                searchValue = value;

                if (String.IsNullOrEmpty(value))
                {
                    SearchResultList = null;
                }
                else
                {
                    SearchResultList = new List<SearchResult>();

                    foreach (var book in Data.Books)
                    {
                        foreach (var page in book.Pages)
                        {
                            if (page.Title.Contains(value) || page.Inner.Contains(value))
                            {

                                var searchResult = new SearchResult()
                                {
                                    BookId = book.Id,
                                    PageId = page.Id,
                                    PageTitle = page.Title,
                                    StringContaining = $"{value}..."
                                };

                                SearchResultList.Add(searchResult);
                            }
                        }
                    }
                }

                //InvokeAsync(Data.con)

            }

        }


        #endregion

        #region Book and page management

        public async Task DeleteBook(Guid bookId)
        {
            var book = Data.Books.Single(x => x.Id == bookId);


            if (Data.History.SelectedBookId == bookId)
            {
                Data.History.SelectedBookId = default;
                Data.History.SelectedPageId = default;
            }

            Data.Books.Remove(book);

            Data.SaveNeeded = true;
            await InvokeAsync(Data.Callback);
        }
        public async Task DeleteBookPage(Guid bookId, Guid pageId)
        {
            var book = Data.Books.Single(x => x.Id == bookId);
            var page = book.Pages.Single(x => x.Id == pageId);


            if (Data.History.SelectedPageId == pageId)
            {
                Data.History.SelectedPageId = default;
                //Data.SelectedPage = null;
            }

            Data.SelectedBook.Pages.Remove(page);

            Data.SaveNeeded = true;
            await InvokeAsync(Data.Callback);
        }

        public async Task HideBook(Guid bookId)
        {
            var book = Data.Books.Single(x => x.Id == bookId);
            book.HiddenPages = !book.HiddenPages;

            await InvokeAsync(Data.Callback);
        }

        public async Task CreatePage(Guid bookId)
        {
            var book = Data.Books.Single(x => x.Id == bookId);
            var newPage = new Page()
            {
                Id = Guid.NewGuid(),
                Inner = String.Empty,
                Saved = false,
                Title = "New page"
            };

            book.Pages.Add(newPage);

            Data.History.SelectedPageId = newPage.Id;

            Data.SaveNeeded = true;
            await InvokeAsync(Data.Callback);
        }

        public async Task CreateBook()
        {
            var book = new Book()
            {
                HiddenPages = false,
                Id = Guid.NewGuid(),
                Name = "New book",
                Pages = new List<Page>()
            };

            var page = new Page()
            {
                Id = Guid.NewGuid(),
                Inner = String.Empty,
                Saved = false,
                Title = "New page"
            };

            book.Pages.Add(page);

            Data.Books.Add(book);
            Data.History.SelectedBookId = book.Id;
            Data.History.SelectedPageId = page.Id;

            Data.SaveNeeded = true;
            await InvokeAsync(Data.Callback);
        }

        public async Task RenameCallback()
        {
            Data.SaveNeeded = true;
            await InvokeAsync(Data.Callback);
        }

        public async Task SelectBookPage(Guid bookId, Guid pageId)
        {
            Data.History.SelectedBookId = bookId;
            Data.History.SelectedPageId = pageId;
            await InvokeAsync(Data.Callback);
        }

        #endregion


    }
}
