using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using RestaurantWebAPI.Models;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: api/orders
        public List<Order> Get()
        {
            return OrderService.GetOrders();
        }

        // GET: api/orders/5
        public Order Get(string id)
        {
            return OrderService.GetOrder(id);
        }

        // POST: api/orders
        public OrderResponse Post(dynamic value)
        {
            var order = JsonConvert.SerializeObject(value);
            return OrderService.PostOrder(order);
        }


        // PUT: api/orders/5
        public void Put(int id, [FromBody]string value)
        {
            // TODO
        }

        // DELETE: api/orders
        public void Delete()
        {
            OrderService.DeleteOrders();
        }

        // DELETE: api/orders/5
        public void Delete(string id)
        {
            OrderService.DeleteOrder(id);
        }

    }
}
