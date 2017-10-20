﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestaurantWebAPI.Models
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