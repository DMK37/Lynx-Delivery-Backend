namespace CourierAppBackend.Models;
public enum OrderStatus
{
    Accepted,
    PickedUp,
    CannotDeliver,
    Delivered
}
public class Order : Base
{
    public int OfferID { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string? Comment { get; set; }
    public DateTime LastUpdate { get; set; }
    public string CourierName { get; set; } = null!;
}
