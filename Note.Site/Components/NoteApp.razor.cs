using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Note.Site.Models;
using Note.Site.Services;
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

        [Inject]
        public DataService DataService { get; set; }

        public static bool SavedStarted = false;

        public static Guid _bookId = Guid.NewGuid();
        public static Guid _pageId = Guid.NewGuid();

        public bool Loading { get; set; } = false;

        public CascadeData Model { get; set; }


        public async Task UseLocalStorage()
        {
            Loading = true;
            StateHasChanged();

            var isDarkModeCurrent = await JSRuntime.InvokeAsync<bool>("getPreferedColor");
            var data = await DataService.GetCascadeDataAsync();

            if (data == null)
            {
                data = await DataService.CreateCascadeDataAsync(isDarkModeCurrent);
            }

            Model = data;
            Model.User.LoggedIn = true;
            Model.Callback = StateHasChanged;

            await JSRuntime.InvokeVoidAsync("setDarkMode", Model.Settings.IsDarkModeEnabled);

            Loading = false;
            StateHasChanged();
        }

        public async Task Logout()
        {
            Loading = true;
            StateHasChanged();

            Model.User.LoggedIn = false;
            await DataService.SetCascadeDataAsync(Model);

            Loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            var data = await DataService.GetCascadeDataAsync();

            if (data != null)
            {
                Model = data;
                Model.Callback = StateHasChanged;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (!SavedStarted)
                {
                    SavedStarted = true;

                    await Task.Run(async () =>
                    {
                        var timer = new Timer(new TimerCallback(async _ =>
                        {
                            if (Model != null)
                            {
                                foreach (var book in Model.Books)
                                {
                                    foreach (var page in book.Pages)
                                    {
                                        if (!page.Saved)
                                        {
                                            page.Saved = true;
                                        }
                                    }
                                }

                                await DataService.SetCascadeDataAsync(Model);
                                StateHasChanged();
                            }
                        }), null, 0, 1000);
                    });
                }
            }
        }
    }
}
