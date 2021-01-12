using Ganss.XSS;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class MarkdownBase : ComponentBase
    {
        [Inject]
        public IHtmlSanitizer HtmlSanitizer { get; set; }

        [Parameter]
        public bool IsMarkdownPreviewEnabled { get; set; }


        private string textareaValue;

        [Parameter]
        public string TextareaValue
        {
            get { return textareaValue; }
            set
            {
                textareaValue = value;
                SetMarkdownValue(value);

            }
        }

        public MarkupString MarkdownValue { get; set; }

        public void SetMarkdownValue(string val)
        {
            if (IsMarkdownPreviewEnabled)
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
    }
}
