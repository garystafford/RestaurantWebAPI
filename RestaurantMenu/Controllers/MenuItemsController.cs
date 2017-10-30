using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Restaurant.Menu.Database;
using Restaurant.Menu.Models;
using Restaurant.Menu.Services;

namespace Restaurant.Menu.Controllers
{
    public class MenuItemsController : ApiController
    {

        private readonly MenuItemsService _menuItemsService = new MenuItemsService(new AtlasConnectionFactory());
        
        // GET: api/menuitems
        public List<MenuItem> Get()
        {
            return _menuItemsService.GetMenuItems();
        }

        // GET: api/menuitems/5
        public MenuItem Get(int id)
        {
            return _menuItemsService.GetMenuItem(id);
        }

        // POST: api/menuitems
        public void Post([FromBody] dynamic value)
        {
            if (value == null)
            {
                _menuItemsService.DeleteMenuItems();
                _menuItemsService.PostMenu();
            }
            else
            {
                var menuItem = JsonConvert.SerializeObject(value);
                _menuItemsService.PostMenuItem(menuItem);
            }
        }

        // PUT: api/menuitems/5
        public void Put(int id, [FromBody] dynamic value)
        {
            var menuItem = JsonConvert.SerializeObject(value);
            _menuItemsService.PutMenuItem(id, menuItem);
        }

        // DELETE: api/menuitems
        public void Delete()
        {
            _menuItemsService.DeleteMenuItems();
        }

        // DELETE: api/menuitems/5
        public void Delete(int id)
        {
            _menuItemsService.DeleteMenuItem(id);
        }
    }
}