using CourierAppBackend.Models;

namespace CourierAppBackend.ModelsPublicDTO
{
    public class GetOrderResponse
    {
        public int OfferID { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? Comment { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CourierName { get; set; } = null!;
    }
}
