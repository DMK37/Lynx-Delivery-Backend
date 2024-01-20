namespace CourierAppBackend.Models.Database;

public enum OfferStatus
{
    Offered,
    Pending,
    Accepted,
    Rejected
}

public class Offer : Base
{
    public Inquiry Inquiry { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; } //15 min
    public DateTime UpdateDate { get; set; }
    public OfferStatus Status { get; set; }
    public string? ReasonOfRejection { get; set; }
    public Price Price { get; set; } = null!;
    public CustomerInfo? CustomerInfo { get; set; }
    public int? OrderID { get; set; }
}