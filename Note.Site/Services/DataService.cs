using Blazored.LocalStorage;
using Newtonsoft.Json;
using Note.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Services
{
    public class DataService
    {
        private readonly ILocalStorageService LocalStorage;
        public DataService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public async Task<CascadeData> GetCascadeDataAsync()
        {
            var item = await LocalStorage.GetItemAsync<CascadeData>("data");

            return item != null ? item : null;
        }

        public async Task<CascadeData> CreateCascadeDataAsync(bool isDarkModeCurrent)
        {

            var result = new CascadeData()
            {
                Settings = new Settings()
                {
                    IsDarkModeEnabled = isDarkModeCurrent,
                    IsMarkdownPreviewEnabled = true,
                    IsScrollAlligmentEnabled = true,
                    IsSidebarCollapsed = false
                },
                Books = new List<Book>(),
                User = new User()
                {
                    Username = "Hyphen's notes",
                    ImageUrl = "https://avatars3.githubusercontent.com/u/77241215?s=200&v=4",
                    LoggedIn = true
                },
                History = new History()
                {
                    SelectedBookId = default,
                    SelectedPageId = default
                }
            };

            await LocalStorage.SetItemAsync<CascadeData>("data", result);

            return result;
        }

        public async Task SetCascadeDataAsync(CascadeData data)
        {
            await LocalStorage.SetItemAsync<CascadeData>("data", data);
        }

    }
}
