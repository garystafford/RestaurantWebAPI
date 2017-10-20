using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantWebAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Order
    {
        public Order()
        {
            OrderNumber = Guid.NewGuid().ToString();
            OrderDateTime = DateTime.Now;
        }

        public Order(IList<OrderItem> orderItems)
        {
            OrderNumber = Guid.NewGuid().ToString();
            OrderDateTime = DateTime.Now;
            OrderItems = orderItems;
        }

        [BsonElement("order_number")]
        public string OrderNumber { get; set; }

        [BsonElement("order_time")]
        public DateTime OrderDateTime { get; set; }

        [BsonElement("order_items")]
        public IList<OrderItem> OrderItems { get; set; }
    }
}