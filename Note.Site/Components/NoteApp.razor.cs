using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Note.Site.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class NoteAppBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

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
                                Saved = true
                            },
                            new Page() {
                                Id = Guid.NewGuid(),
                                Title = "Page 2",
                                Inner = "# Hello Development 2",
                                Saved = true
                            },
                            new Page() {
                                Id = Guid.NewGuid(),
                                Title = "Page 3",
                                Inner = "# Hello Development 3",
                                Saved = true
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
            },
            SaveNeeded = false
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Model.Callback = MyStateHasChanged;
                await JSRuntime.InvokeVoidAsync("setDarkMode", Model.Settings.IsDarkModeEnabled);

                await Task.Run(async () =>
                {
                    var timer = new Timer(new TimerCallback(async _ =>
                    {
                        if (Model.SaveNeeded)
                        {
                            foreach (var book in Model.Books)
                            {
                                foreach (var page in book.Pages)
                                {
                                    if (!page.Saved)
                                    {
                                        // Logic to save here!

                                        page.Saved = true;
                                        StateHasChanged();
                                    }
                                }
                            }
                        }

                        Model.SaveNeeded = false;
                    }), null, 0, 2000);
                });
            }
        }

        public void MyStateHasChanged()
        {
            StateHasChanged();
        }
    }
}
