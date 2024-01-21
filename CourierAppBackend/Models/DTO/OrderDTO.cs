using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Models.DTO
{
    public class OrderDTO
    {
        public int OfferID { get; set; }
        public OfferDTO Offer { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
        public string? Comment { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CourierName { get; set; } = null!;
    }
}
