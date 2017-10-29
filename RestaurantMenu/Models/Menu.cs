using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Restaurant.Menu.Models
{
    public class Menu : Collection<MenuItem>
    {
        public Menu()
        {
        }

        public Menu(IList<MenuItem> menuItems)
            : base(menuItems)
        {
        }
    }
}