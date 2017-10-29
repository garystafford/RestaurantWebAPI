using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Order.Models
{
    [BsonIgnoreExtraElements]
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(int quantity, int menuId, string description, double price)
        {
            Quantity = quantity;
            MenuId = menuId;
            Description = description;
            Price = price;
        }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("menu_id")]
        public int MenuId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }
    }
}