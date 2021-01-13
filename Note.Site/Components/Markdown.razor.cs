using Ganss.XSS;
using Microsoft.AspNetCore.Components;
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

        #region Header

        public void SetDarkModeToggle() => Data.Settings.IsDarkModeEnabled = !Data.Settings.IsDarkModeEnabled;

        public void SetMarkdownPreviewToggle() => Data.Settings.IsMarkdownPreviewEnabled = !Data.Settings.IsMarkdownPreviewEnabled;

        public void SetScrollAlligmentToggle() => Data.Settings.IsScrollAlligmentEnabled = !Data.Settings.IsScrollAlligmentEnabled;


        #endregion

        #region Markdown

        [Inject]
        public IHtmlSanitizer HtmlSanitizer { get; set; }

        public string TextareaValue
        {
            get { return Data.SelectedPage.Inner; }
            set
            {
                Data.SelectedPage.Inner = value;
                SetMarkdownValue(value);

            }
        }

        public MarkupString MarkdownValue { get; set; }

        public void SetMarkdownValue(string val)
        {
            if (Data.Settings.IsMarkdownPreviewEnabled)
            {
                if (!string.IsNullOrWhiteSpace(val))
                {
                    // Convert markdown string to HTML
                    var html = Markdig.Markdown.ToHtml(val);

                    // Sanitize HTML before rendering
                    var sanitizedHtml = HtmlSanitizer.Sanitize(html);

                    // Return sanitized HTML as a MarkupString that Blazor can render
                    MarkdownValue = new MarkupString(sanitizedHtml);
                }
                else
                {
                    MarkdownValue = new MarkupString();
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            SetMarkdownValue(TextareaValue);
        }

        #endregion
    }
}
