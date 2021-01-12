using Microsoft.AspNetCore.Components;
using Note.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class HeaderBase : ComponentBase
    {
        public void SetDarkModeToggle() => Data.Settings.IsDarkModeEnabled = !Data.Settings.IsDarkModeEnabled;

        public void SetMarkdownPreviewToggle() => Data.Settings.IsMarkdownPreviewEnabled = !Data.Settings.IsMarkdownPreviewEnabled;

        public void SetScrollAlligmentToggle() => Data.Settings.IsScrollAlligmentEnabled = !Data.Settings.IsScrollAlligmentEnabled;

        [CascadingParameter]
        public CascadeData Data { get; set; }
    }
}
