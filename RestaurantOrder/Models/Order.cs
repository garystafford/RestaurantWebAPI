using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Order.Models
{
    [BsonIgnoreExtraElements]
    public class Order
    {
        public Order()
        {
            OrderNumber = Guid.NewGuid().ToString();
            TimePlaced = DateTime.Now;
        }

        public Order(IList<OrderItem> items)
        {
            OrderNumber = Guid.NewGuid().ToString();
            TimePlaced = DateTime.Now;
            Items = items;
        }

        [BsonElement("order_number")]
        public string OrderNumber { get; set; }

        [BsonElement("time_placed")]
        public DateTime TimePlaced { get; set; }

        [BsonElement("items")]
        public IList<OrderItem> Items { get; set; }
    }
}