using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Restaurant.WebAPI.Tests
{
    [TestClass()]
    public class UtilitiesTests
    {
        [TestMethod()]
        public void NormalizeJsonPStringTest()
        {
            const string requestBody = "restaurantOrder={%22restaurantOrder%22:[{%22Quantity%22:%222%22,%22Description%22:%22Coffee%22," +
                                       "%22Price%22:%220.99%22},{%22Quantity%22:%222%22,%22Description%22:%22French%20Fries%22,%22Price%22:%221.29%22}," +
                                       "{%22Quantity%22:%223%22,%22Description%22:%22Hamburger%22,%22Price%22:%222.99%22},{%22Quantity%22:%221%22," +
                                       "%22Description%22:%22Soft%20Drink%22,%22Price%22:%221.19%22},{%22Quantity%22:%222%22," +
                                       "%22Description%22:%22Ice%20Cream%20Cone%22,%22Price%22:%221.99%22}]}&callback=OrderResponse&_=1508550773268";

            const string expectedResult = "[{\"Quantity\":\"2\",\"Description\":\"Coffee\",\"Price\":\"0.99\"},{\"Quantity\":\"2\"," +
                                          "\"Description\":\"French Fries\",\"Price\":\"1.29\"},{\"Quantity\":\"3\",\"Description\":\"Hamburger\"," +
                                          "\"Price\":\"2.99\"},{\"Quantity\":\"1\",\"Description\":\"Soft Drink\",\"Price\":\"1.19\"},{\"Quantity\":\"2\"" +
                                          ",\"Description\":\"Ice Cream Cone\",\"Price\":\"1.99\"}]";

                Assert.AreEqual(expectedResult, Utilities.NormalizeJsonPString(requestBody));
        }
    }
}