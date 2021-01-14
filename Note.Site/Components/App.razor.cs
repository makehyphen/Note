using Microsoft.AspNetCore.Components;
using Note.Site.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class AppBase : ComponentBase
    {
        public static Guid _bookId = Guid.NewGuid();
        public static Guid _pageId = Guid.NewGuid();

        public CascadeData Model { get; set; } = new CascadeData()
        {
            Settings = new Settings()
            {
                IsDarkModeEnabled = true,
                IsMarkdownPreviewEnabled = true,
                IsScrollAlligmentEnabled = true,
                IsSidebarCollapsed = false
            },
            Books = new List<Book>() {
                new Book() {
                    Id = _bookId,
                    Name = "Development",
                    Pages =
                        new List<Page>() {
                            new Page() {
                                Id = _pageId,
                                Title = "Page",
                                Inner = "# Hello Development",
                                Saved = false
                            }
                        }
                    }
                },
            User = new User()
            {
                Username = "Emiliano",
                ImageUrl = "https://avatars2.githubusercontent.com/u/13499480?s=460&u=dc817bc92bd7144f068f26107fa0d6bbf7be8471&v=4?v=3&s=48"
            },
            History = new History()
            {
                SelectedBookId = _bookId,
                SelectedPageId = _pageId
            }
        };


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Model.Callback = MyStateHasChanged;
            }
        }

        public void MyStateHasChanged()
        {
            StateHasChanged();
        }

    }
}
