using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantWebAPI.Models
{
    [BsonIgnoreExtraElements]
    public class MenuItem
    {
        public MenuItem()
        {
        }

        public MenuItem(string description, double price)
        {
            Description = description;
            Price = price;
        }


        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }
    }
}