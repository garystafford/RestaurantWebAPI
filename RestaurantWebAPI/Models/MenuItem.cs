using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantWebAPI.Models
{
    [BsonIgnoreExtraElements]
    public class MenuItem
    {
        public MenuItem()
        {
        }

        public MenuItem(int id, string description, double price)
        {
            Id = id;
            Description = description;
            Price = price;
        }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }
    }
}