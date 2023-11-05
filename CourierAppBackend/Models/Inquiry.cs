
namespace CourierAppBackend.Models;

// ?
public enum InquiryStatus
{
    Created,
    OptionsOffered,
    OfferAccepted
}
public class Inquiry : Base
{
    // There is no user for not logged in user
    public User? User { get; set; }
    public DateTime DateOfInquiring { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Package Package { get; set; }
    public Address SourceAddress { get; set; }
    public Address DestinationAddress { get; set; }
    public bool IsCompany { get; set; } // ?
    public bool DeliveryAtWeekend { get; set; }
}
