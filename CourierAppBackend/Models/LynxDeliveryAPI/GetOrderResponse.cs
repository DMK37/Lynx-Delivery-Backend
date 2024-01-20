using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Models.LynxDeliveryAPI;

public class GetOrderResponse
{
    public int OfferID { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string? Comment { get; set; }
    public DateTime LastUpdate { get; set; }
    public string CourierName { get; set; } = null!;
}
