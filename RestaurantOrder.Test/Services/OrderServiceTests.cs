using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.WebAPI.Services;

namespace Restaurant.Order.Tests.Services
{
    [TestClass()]
    public class OrderServiceTests
    {
        private readonly string _order1 = "[{\"menuId\": \"1\",\"description\": \"Cheeseburger\",\"quantity\": \"1\",\"price\": \"3.99\"}," +
                                         "{\"menuId\": \"2\",\"description\": \"Hot Dog\",\"quantity\": \"2\",\"price\": \"2.49\"}]";

        private readonly string _order2 = "[{\"menuId\": \"3\",\"description\": \"Coffee\",\"quantity\": \"3\",\"price\": \"1.99\"}," +
                                         "{\"menuId\": \"4\",\"description\": \"Ice Cream\",\"quantity\": \"2\",\"price\": \"3.29\"}]";

        [TestInitialize()]
        public void TestInitialize()
        {
            OrdersService.DeleteOrders();
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            OrdersService.DeleteOrders();
        }

        [TestMethod()]
        public void PostOrderTest()
        {
            OrdersService.PostOrder(_order1);
            var orders = OrdersService.GetOrders();

            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(2, orders[0].Items.Count);
            Assert.AreEqual(1, orders[0].Items[0].Quantity);
            Assert.AreEqual(1, orders[0].Items[0].MenuId);
            Assert.AreEqual("Cheeseburger", orders[0].Items[0].Description);
            Assert.AreEqual(3.99, orders[0].Items[0].Price);
        }

        [TestMethod()]
        public void GetOrderTest()
        {
            OrdersService.PostOrder(_order1);
            var orders = OrdersService.GetOrders();
            var orderNumber = orders[0].OrderNumber;

            OrdersService.GetOrder(orderNumber);

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
            OrdersService.PostOrder(_order1);
            OrdersService.PostOrder(_order2);

            var orders = OrdersService.GetOrders();

            Assert.AreEqual(2, orders.Count);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            OrdersService.PostOrder(_order1);
            var orders = OrdersService.GetOrders();
            var orderNumber = orders[0].OrderNumber;
            OrdersService.DeleteOrder(orderNumber);
            orders = OrdersService.GetOrders();

            Assert.AreEqual(0, orders.Count);
        }

        [TestMethod()]
        public void DeleteOrdersTest()
        {
            OrdersService.PostOrder(_order1);
            OrdersService.PostOrder(_order2);

            OrdersService.DeleteOrders();
            var orders = OrdersService.GetOrders();

            Assert.AreEqual(0, orders.Count);
        }
    }
}