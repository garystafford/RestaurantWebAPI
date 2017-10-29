using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Restaurant.Order.Models;
using Restaurant.Order.Services;

namespace Restaurant.Order.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: api/orders
        public List<Models.Order> Get()
        {
            return OrdersService.GetOrders();
        }

        // GET: api/orders/5
        public Models.Order Get(string id)
        {
            return OrdersService.GetOrder(id);
        }

        // POST: api/orders
        public OrderResponse Post(dynamic value)
        {
            var order = JsonConvert.SerializeObject(value);
            return OrdersService.PostOrder(order);
        }


        // PUT: api/orders/5
        public void Put(int id, [FromBody]string value)
        {
            // TODO
        }

        // DELETE: api/orders
        public void Delete()
        {
            OrdersService.DeleteOrders();
        }

        // DELETE: api/orders/5
        public void Delete(string id)
        {
            OrdersService.DeleteOrder(id);
        }

    }
}
