using Microsoft.AspNetCore.Components;
using Note.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class SidebarBase : ComponentBase
    {
        [CascadingParameter]
        public CascadeData Data { get; set; }

        public bool IsCollapsed { get; set; } = true;

        public void UpdateCollapsed()
        {
            IsCollapsed = !IsCollapsed;
            //StateHasChanged();
        }
    }
}
