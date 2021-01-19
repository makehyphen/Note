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


        //public string inputBookValue;
        //public string InputBookValue
        //{
        //    set
        //    {
        //        inputBookValue = value;
        //        Data.SelectedBook.Name = inputBookValue;
        //        Data.SelectedPage.Saved = false;
        //        Data.SaveNeeded = true;
        //    }
        //    get
        //    {
        //        return Data.SelectedBook != null ? Data.SelectedBook.Name : inputBookValue;
        //    }
        //}

        //public string inputPageValue;
        //public string InputPageValue
        //{
        //    set
        //    {
        //        inputPageValue = value;
        //        Data.SelectedPage.Title = inputPageValue;
        //        Data.SelectedPage.Saved = false;
        //        Data.SaveNeeded = true;
        //    }
        //    get
        //    {
        //        return Data.SelectedPage != null ? Data.SelectedPage.Title : inputBookValue;
        //    }
        //}


        public string textareaValue;
        public string TextareaValue
        {
            set
            {
                textareaValue = value;
                Data.SelectedPage.Inner = textareaValue;
                Data.SelectedPage.Saved = false;
                Data.SaveNeeded = true;
            }

            get
            {
                if (Data.SelectedPage != null)
                {
                    return Data.SelectedPage.Inner;
                }

                return textareaValue;
            }
        }

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("renderMarkdownNow", TextareaValue);
                await JSRuntime.InvokeVoidAsync("setState", Data.Settings.IsScrollAlligmentEnabled);
                await JSRuntime.InvokeVoidAsync("reset");
                await JSRuntime.InvokeVoidAsync("setWidthInput");
            }

            // Add event listener again, will be discarded if already added :)
            await JSRuntime.InvokeVoidAsync("renderMarkdown", true);
        }

        protected override async Task OnParametersSetAsync()
         {
            if (Data.SelectedPage != null)
            {
                await JSRuntime.InvokeVoidAsync("renderMarkdownNow", TextareaValue);
            }
        }



        #endregion
    }
}
