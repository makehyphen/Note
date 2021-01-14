using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Note.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class MarkdownBase : ComponentBase
    {
        [CascadingParameter]
        public CascadeData Data { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        #region Header

        public async Task SetDarkModeToggle()
        {
            Data.Settings.IsDarkModeEnabled = !Data.Settings.IsDarkModeEnabled;
            await JSRuntime.InvokeVoidAsync("setDarkMode", Data.Settings.IsDarkModeEnabled);
        }

        public void SetMarkdownPreviewToggle() => Data.Settings.IsMarkdownPreviewEnabled = !Data.Settings.IsMarkdownPreviewEnabled;

        public async Task SetScrollAlligmentToggle()
        {
            Data.Settings.IsScrollAlligmentEnabled = !Data.Settings.IsScrollAlligmentEnabled;
            await JSRuntime.InvokeVoidAsync("setState", Data.Settings.IsScrollAlligmentEnabled);
        }

        #endregion

        #region Markdown

        public string TextareaValue { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("renderMarkdown");
                await JSRuntime.InvokeVoidAsync("setState", Data.Settings.IsScrollAlligmentEnabled);
                await JSRuntime.InvokeVoidAsync("reset");
            }
        }



        #endregion
    }
}
