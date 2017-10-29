using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Newtonsoft.Json;
using Restaurant.WebAPI.Database;
using Restaurant.WebAPI.Models;

namespace Restaurant.WebAPI.Services
{
    public static class OrdersService
    {

        public static OrderResponse PostOrder(string restaurantOrder)
        {
            try
            {
                var order = DeserializeOrder(restaurantOrder);
                var collectionOrders = GetCollectionOrders();
                collectionOrders.InsertOne(order);

                return new OrderResponse
                {
                    TimePlaced = order.TimePlaced,
                    OrderNumber = order.OrderNumber,
                    Message = "Thank you for your order."

                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    TimePlaced = DateTime.Now,
                    OrderNumber = Guid.NewGuid().ToString(),
                    Message = "Sorry, an error has occurred. Please place your order again."
                };
            }


        }

        public static Order GetOrder(string orderNumber)
        {
            var collectionOrders = GetCollectionOrders();
            return collectionOrders.Find(order => order.OrderNumber == orderNumber).SingleOrDefault();
        }

        public static List<Order> GetOrders()
        {
            var collectionOrders = GetCollectionOrders();
            return collectionOrders.Find(order => order.OrderNumber != null).ToList();
        }

        public static void DeleteOrder(string orderNumber)
        {
            var collectionOrders = GetCollectionOrders();
            collectionOrders.FindOneAndDelete(order => order.OrderNumber == orderNumber);
        }

        public static void DeleteOrders()
        {
            AtlasConnectionFactory.GetDatabase("restaurant").DropCollection("orders");
        }

        private static IMongoCollection<Order> GetCollectionOrders()
        {
            return AtlasConnectionFactory.GetDatabase("restaurant").GetCollection<Order>("orders");
        }

        private static Order DeserializeOrder(string restaurantOrder)
        {
            restaurantOrder = Utilities.NormalizeJsonPString(restaurantOrder);
            var order = JsonConvert.DeserializeObject<List<OrderItem>>(restaurantOrder);
            return new Order(order);
        }
    }
}