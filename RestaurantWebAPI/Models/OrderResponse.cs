namespace RestaurantWebAPI.Models
{
    public class OrderResponse
    {
        public OrderResponse()
        {
        }

        public OrderResponse(string orderDateTime, string orderItems, string orderMessage)
        {
            OrderDateTime = orderDateTime;
            OrderNumber = orderItems;
            OrderMessage = orderMessage;
        }

        public string OrderDateTime { get; set; }

        public string OrderNumber { get; set; }

        public string OrderMessage { get; set; }
    }
}