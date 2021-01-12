using Microsoft.AspNetCore.Components;
using Note.Site.Models;
using System;
using System.Collections.Generic;

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
                IsSidebarCollapsed = true
            },
            Books = new List<Book>() {
                new Book(){
                    Id = _bookId,
                    Name = "Development",
                    Pages =
                        new List<Page>(){
                            new Page() {
                                Id = _pageId,
                                Title = "Page",
                                Inner = "# Hello Development",
                                Saved = false
                            }
                        }
                    } },
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

        #region Sidebar

        public string Username { get; set; } = "Emiliano";
        public string ImageUrl { get; set; } = "https://avatars2.githubusercontent.com/u/13499480?s=460&u=dc817bc92bd7144f068f26107fa0d6bbf7be8471&v=4?v=3&s=48";
        public bool IsCollapsed { get; set; }

        #endregion

        #region Header

        public string BookTitle { get; set; } = "Book";

        public string PageTitle { get; set; } = "Page";

        public bool IsSaved { get; set; } = true;

        public bool IsDarkModeEnabled { get; set; } = false;

        public bool IsMarkdownPreviewEnabled { get; set; } = true;

        public bool IsScrollAlligmentEnabled { get; set; } = true;


        public void OnDarkModeToggle()
        {
            IsDarkModeEnabled = !IsDarkModeEnabled;
        }

        public void OnMarkdownPreviewToggle()
        {
            IsMarkdownPreviewEnabled = !IsMarkdownPreviewEnabled;
            StateHasChanged();
            //SetMarkdownValue(textareaValue);
        }

        public void OnScrollAlligmentToggle()
        {
            IsScrollAlligmentEnabled = !IsScrollAlligmentEnabled;
        }

        #endregion

        #region Markdown

        public string Markdown { get; set; } = "### Hello Markdown";

        #endregion


    }
}
