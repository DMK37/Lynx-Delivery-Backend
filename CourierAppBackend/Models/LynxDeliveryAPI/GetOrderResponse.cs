
namespace CourierAppBackend.Models.LynxDeliveryAPI;

public class GetOrderResponse
{
    public int OfferID { get; set; }
    public string OrderStatus { get; set; } = null!;
    public string? Comment { get; set; }
    public DateTime LastUpdate { get; set; }
    public string CourierName { get; set; } = null!;
}
