using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Restaurant.Order.Database;
using Restaurant.Order.Models;
using Restaurant.Order.Services;

namespace Restaurant.Order.Controllers
{
    public class OrdersController : ApiController
    {

        private readonly OrdersService _ordersService = new OrdersService(new AtlasConnectionFactory());

        // GET: api/orders
        public List<Models.Order> Get()
        {
            return _ordersService.GetOrders();
        }

        // GET: api/orders/5
        public Models.Order Get(string id)
        {
            return _ordersService.GetOrder(id);
        }

        // POST: api/orders
        public OrderResponse Post(dynamic value)
        {
            var order = JsonConvert.SerializeObject(value);
            return _ordersService.PostOrder(order);
        }


        // PUT: api/orders/5
        public void Put(string orderNumber, [FromBody] dynamic order)
        {
            _ordersService.PutOrder(orderNumber, order);
        }

        // DELETE: api/orders
        public void Delete()
        {
            _ordersService.DeleteOrders();
        }

        // DELETE: api/orders/5
        public void Delete(string id)
        {
            _ordersService.DeleteOrder(id);
        }
    }
}