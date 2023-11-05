namespace CourierAppBackend.Models;

public enum OfferStatus
{
    Offered,
    Pending,
    Accepted,
    Rejected,
    PickedUp,
    Delivered
}

public class Offer : Base
{
    public Inquiry Inquiry { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Package Package { get; set; }
    public Address SourceAddress { get; set; }
    public Address DestinationAddress { get; set; }
    public bool DeliveryAtWeekend { get; set; }
    public OfferStatus Status { get; set; }
    public Price Price { get; set; }
}