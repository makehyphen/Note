using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Site.Components
{
    public class SidebarUserBase : ComponentBase
    {
        [Parameter]
        public bool IsCollapsed { get; set; }

        [Parameter]
        public string Username { get; set; }

        [Parameter]
        public string ImagePath { get; set; }

        [Parameter]
        public EventCallback<bool> OnClickCallback { get; set; }




        public async Task Collapse()
        {
            await OnClickCallback.InvokeAsync();
        }

        protected override async Task OnParametersSetAsync()
        {

        }
    }
}
