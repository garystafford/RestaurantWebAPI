using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Order.Database;
using Restaurant.Order.Services;

namespace Restaurant.Order.Tests.Services
{
    [TestClass()]
    public class OrderServiceTests
    {
        private const string Order1 = "{\"items\":[{\"menuId\": \"1\",\"description\": \"Cheeseburger\",\"quantity\": \"1\",\"price\": \"3.99\"}," +
            "{\"menuId\": \"2\",\"description\": \"Hot Dog\",\"quantity\": \"2\",\"price\": \"2.49\"}]}";

        private const string Order2 = "{\"items\":[{\"menuId\": \"3\",\"description\": \"Coffee\",\"quantity\": \"3\",\"price\": \"1.99\"}," +
            "{\"menuId\": \"4\",\"description\": \"Ice Cream\",\"quantity\": \"2\",\"price\": \"3.29\"}]}";

        private readonly OrdersService _ordersService = new OrdersService(new MongoAuthConnectionFactory());

        [TestInitialize()]
        public void TestInitialize()
        {
            _ordersService.DeleteOrders();
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            _ordersService.DeleteOrders();
        }

        [TestMethod()]
        public void PostOrderTest()
        {
            _ordersService.PostOrder(Order1);
            var orders = _ordersService.GetOrders();

            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(2, orders[0].Items.Count);
            Assert.AreEqual(1, orders[0].Items[0].Quantity);
            Assert.AreEqual(1, orders[0].Items[0].MenuId);
            Assert.AreEqual("Cheeseburger", orders[0].Items[0].Description);
            Assert.AreEqual(3.99, orders[0].Items[0].Price);
        }

        [TestMethod()]
        public void PutOrderTest()
        {
            const string order2Revised = "{\"items\":[{\"menuId\": \"3\",\"description\": \"Gourmet Coffee\",\"quantity\": \"3\",\"price\": \"2.59\"}," +
            "{\"menuId\": \"4\",\"description\": \"Soft-Serve Ice Cream\",\"quantity\": \"2\",\"price\": \"3.29\"}]}";

            _ordersService.PostOrder(Order2);
            var orders = _ordersService.GetOrders();
            var orderNumber = orders[0].OrderNumber;

            _ordersService.PutOrder(orderNumber, order2Revised);

            orders = _ordersService.GetOrders();

            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(2, orders[0].Items.Count);
            Assert.AreEqual(3, orders[0].Items[0].Quantity);
            Assert.AreEqual(3, orders[0].Items[0].MenuId);
            Assert.AreEqual("Gourmet Coffee", orders[0].Items[0].Description);
            Assert.AreEqual(2.59, orders[0].Items[0].Price);
        }

        [TestMethod()]
        public void GetOrderTest()
        {
            _ordersService.PostOrder(Order1);
            var orders = _ordersService.GetOrders();
            var orderNumber = orders[0].OrderNumber;

            _ordersService.GetOrder(orderNumber);

            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(2, orders[0].Items.Count);
            Assert.AreEqual(2, orders[0].Items[1].Quantity);
            Assert.AreEqual(2, orders[0].Items[1].MenuId);
            Assert.AreEqual("Hot Dog", orders[0].Items[1].Description);
            Assert.AreEqual(2.49, orders[0].Items[1].Price);
        }

        [TestMethod()]
        public void GetOrdersTest()
        {
            _ordersService.PostOrder(Order1);
            _ordersService.PostOrder(Order2);

            var orders = _ordersService.GetOrders();

            Assert.AreEqual(2, orders.Count);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            _ordersService.PostOrder(Order1);
            var orders = _ordersService.GetOrders();
            var orderNumber = orders[0].OrderNumber;
            _ordersService.DeleteOrder(orderNumber);
            orders = _ordersService.GetOrders();

            Assert.AreEqual(0, orders.Count);
        }

        [TestMethod()]
        public void DeleteOrdersTest()
        {
            _ordersService.PostOrder(Order1);
            _ordersService.PostOrder(Order2);

            _ordersService.DeleteOrders();
            var orders = _ordersService.GetOrders();

            Assert.AreEqual(0, orders.Count);
        }
    }
}