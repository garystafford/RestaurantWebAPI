using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantWebAPI.Models
{
    [BsonIgnoreExtraElements]
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(int id, int quantity, string description, double price)
        {
            Id = id;
            Quantity = quantity;
            Price = price;
            Description = description;
        }

        [BsonElement("id")]
        public int Id { get; set; }


        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }
    }
}