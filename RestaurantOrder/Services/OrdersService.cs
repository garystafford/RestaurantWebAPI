using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Restaurant.Order.Database;
using Restaurant.Order.Models;

namespace Restaurant.Order.Services
{
    public class OrdersService
    {
        private readonly IMongoConnectionFactory _mongoConnectionFactory;

        public OrdersService(IMongoConnectionFactory mongoConnectionFactory)
        {
            _mongoConnectionFactory = mongoConnectionFactory;
        }

        private IMongoDatabase GetMongoDatabase()
        {
            return _mongoConnectionFactory.GetDatabase("order");
        }

        private IMongoCollection<Models.Order> GetOrdersCollection()
        {
            return GetMongoDatabase().GetCollection<Models.Order>("orders");
        }

        private IMongoCollection<BsonDocument> GetOrdersCollectionAsBson()
        {
            return GetMongoDatabase().GetCollection<BsonDocument>("orders");
        }

        public OrderResponse PostOrder(string restaurantOrder)
        {
            try
            {
                var order = DeserializeOrder(restaurantOrder);
                var collectionOrders = GetOrdersCollection();
                collectionOrders.InsertOne(order);

                return new OrderResponse
                {
                    TimePlaced = order.TimePlaced,
                    OrderNumber = order.OrderNumber,
                    Message = "Thank you for your order."
                };
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                return new OrderResponse
                {
                    TimePlaced = DateTime.Now,
                    OrderNumber = Guid.NewGuid().ToString(),
                    Message = "Sorry, an error has occurred. Please place your order again."
                };
            }
        }

        public Models.Order GetOrder(string orderNumber)
        {
            var collectionOrders = GetOrdersCollection();
            return collectionOrders.Find(order => order.OrderNumber == orderNumber).SingleOrDefault();
        }

        public List<Models.Order> GetOrders()
        {
            var collectionOrders = GetOrdersCollection();
            return collectionOrders.Find(order => order.OrderNumber != null).ToList();
        }

        public void DeleteOrder(string orderNumber)
        {
            var collectionOrders = GetOrdersCollection();
            collectionOrders.FindOneAndDelete(order => order.OrderNumber == orderNumber);
        }

        public void DeleteOrders()
        {
            GetMongoDatabase().DropCollection("orders");
        }

        public void PutOrder(string orderNumber, string order)
        {
            var deserializeOrder = DeserializeOrder(order);
            var collection = GetOrdersCollectionAsBson();

            var filter = Builders<BsonDocument>.Filter.Eq("order_number", orderNumber);
            var update = Builders<BsonDocument>.Update
                .Set("items", deserializeOrder.Items)
                .CurrentDate(DateTime.Now.ToJson());
            var result = collection.UpdateOne(filter, update);
            Console.Write(result.IsAcknowledged);
        }

        private static Models.Order DeserializeOrder(string order)
        {
            return JsonConvert.DeserializeObject<Models.Order>(order);
        }
    }
}