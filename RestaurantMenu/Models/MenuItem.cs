using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Menu.Models
{
    [BsonIgnoreExtraElements]
    public class MenuItem
    {
        public MenuItem()
        {
        }

        public MenuItem(int menuId, string description, double price)
        {
            MenuId = menuId;
            Description = description;
            Price = price;
        }

        [BsonElement("menu_id")]
        public int MenuId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }
    }
}