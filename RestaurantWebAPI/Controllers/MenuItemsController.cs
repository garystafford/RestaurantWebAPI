using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using RestaurantWebAPI.Models;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Controllers
{
    public class MenuItemsController : ApiController
    {
        // GET: api/menuitems
        public List<MenuItem> Get()
        {
            return MenuItemsService.GetMenuItems();
        }

        // GET: api/menuitems/5
        public MenuItem Get(int id)
        {
            return MenuItemsService.GetMenuItem(id);
        }

        // POST: api/menuitems
        public void Post([FromBody]dynamic value)
        {
            if (value == null)
            {
                MenuItemsService.DeleteMenuItems();
                MenuItemsService.PostMenu();
            }
            else
            {
                var menuItem = JsonConvert.SerializeObject(value);
                MenuItemsService.PostMenuItem(menuItem);
            }
        }

        // PUT: api/menuitems/5
        public void Put(int id, [FromBody]string value)
        {
            // TODO
        }

        // DELETE: api/menuitems
        public void Delete()
        {
            MenuItemsService.DeleteMenuItems();
        }

        // DELETE: api/menuitems/5
        public void Delete(int id)
        {
            MenuItemsService.DeleteMenuItem(id);
        }

    }
}
