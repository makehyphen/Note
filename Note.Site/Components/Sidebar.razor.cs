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

        public void UpdateCollapsed()
        {
            Data.Settings.IsSidebarCollapsed = !Data.Settings.IsSidebarCollapsed;
            //StateHasChanged();
        }

        #region Book

        public void CreateBook() { }
        public void DeleteBook() { }

        #endregion

        #region Page

        public void CreateBookPage() { }
        public void DeleteBookPage() { }

        public async Task SelectBookPage(Guid id)
        {
            Data.History.SelectedPageId = id;
            Data.Callback();
        }

        #endregion


    }
}
