using System;

namespace Restaurant.Order.Models
{
    public class OrderResponse
    {
        public OrderResponse()
        {
        }

        public OrderResponse(DateTime timePlaced, string orderNumber, string message)
        {
            TimePlaced = timePlaced;
            OrderNumber = orderNumber;
            Message = message;
        }

        public DateTime TimePlaced { get; set; }

        public string OrderNumber { get; set; }

        public string Message { get; set; }
    }
}