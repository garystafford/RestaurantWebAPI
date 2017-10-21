using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebAPI.Services;

namespace RestaurantWebAPI.Tests.Services
{
    [TestClass()]
    public class OrderServiceTests
    {
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
            const string order = "[{\"Quantity\": \"1\",\"Description\": \"Cheeseburger\",\"Price\": \"3.99\"}," +
                                 "{\"Quantity\": \"2\",\"Description\": \"Hot Dog\",\"Price\": \"2.49\"}]";
            OrdersService.PostOrder(order);
            var orders = OrdersService.GetOrders();

            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(2, orders[0].OrderItems.Count);
            Assert.AreEqual(1, orders[0].OrderItems[0].Quantity);
            Assert.AreEqual("Cheeseburger", orders[0].OrderItems[0].Description);
            Assert.AreEqual(3.99, orders[0].OrderItems[0].Price);
        }

        [TestMethod()]
        public void GetOrderTest()
        {
            const string order = "[{\"Quantity\": \"1\",\"Description\": \"Cheeseburger\",\"Price\": \"3.99\"}," +
                                 "{\"Quantity\": \"2\",\"Description\": \"Hot Dog\",\"Price\": \"2.49\"}]";
            OrdersService.PostOrder(order);
            var orders = OrdersService.GetOrders();
            var orderNumber = orders[0].OrderNumber;

            OrdersService.GetOrder(orderNumber);

            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(2, orders[0].OrderItems.Count);
            Assert.AreEqual(2, orders[0].OrderItems[1].Quantity);
            Assert.AreEqual("Hot Dog", orders[0].OrderItems[1].Description);
            Assert.AreEqual(2.49, orders[0].OrderItems[1].Price);
        }

        [TestMethod()]
        public void GetOrdersTest()
        {
            var order = "[{\"Quantity\": \"1\",\"Description\": \"Cheeseburger\",\"Price\": \"3.99\"}," +
                        "{\"Quantity\": \"2\",\"Description\": \"Hot Dog\",\"Price\": \"2.49\"}]";
            OrdersService.PostOrder(order);

            order = "[{\"Quantity\": \"3\",\"Description\": \"Coffee\",\"Price\": \"1.99\"}," +
                    "{\"Quantity\": \"2\",\"Description\": \"Ice Cream\",\"Price\": \"3.29\"}]";
            OrdersService.PostOrder(order);

            var orders = OrdersService.GetOrders();

            Assert.AreEqual(2, orders.Count);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            const string order = "[{\"Quantity\": \"1\",\"Description\": \"Cheeseburger\",\"Price\": \"3.99\"}," +
                                 "{\"Quantity\": \"2\",\"Description\": \"Hot Dog\",\"Price\": \"2.49\"}]";
            OrdersService.PostOrder(order);
            var orders = OrdersService.GetOrders();
            var orderNumber = orders[0].OrderNumber;
            OrdersService.DeleteOrder(orderNumber);
            orders = OrdersService.GetOrders();

            Assert.AreEqual(0, orders.Count);
        }

        [TestMethod()]
        public void DeleteOrdersTest()
        {
            var order = "[{\"Quantity\": \"1\",\"Description\": \"Cheeseburger\",\"Price\": \"3.99\"}," +
                        "{\"Quantity\": \"2\",\"Description\": \"Hot Dog\",\"Price\": \"2.49\"}]";
            OrdersService.PostOrder(order);

            order = "[{\"Quantity\": \"3\",\"Description\": \"Coffee\",\"Price\": \"1.99\"}," +
                    "{\"Quantity\": \"2\",\"Description\": \"Ice Cream\",\"Price\": \"3.29\"}]";
            OrdersService.PostOrder(order);

            OrdersService.DeleteOrders();
            var orders = OrdersService.GetOrders();

            Assert.AreEqual(0, orders.Count);
        }
    }
}