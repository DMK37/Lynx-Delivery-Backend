namespace CourierAppBackend.Models.Database;
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
    public Offer Offer { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; }
    public string? Comment { get; set; }
    public DateTime LastUpdate { get; set; }
    public string CourierName { get; set; } = null!;
}
