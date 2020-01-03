using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class MenuItem
    {
        public string CommandParameter { get; set; }
        public string Icon { get; set; }
        public string DisplayName { get; set; }
        
        public MenuItem(string displayName, string commandParameter, string icon)
        {
            DisplayName = displayName;
            CommandParameter = commandParameter;
            Icon = icon;
        }
    }
}
