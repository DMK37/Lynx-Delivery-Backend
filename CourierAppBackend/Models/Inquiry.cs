
using System.ComponentModel.DataAnnotations.Schema;

namespace CourierAppBackend.Models;

// ?
public enum InquiryStatus
{
    Created,
    Accepted
}
public class Inquiry : Base
{
    // There is no user for not logged in user
    public string? UserId { get; set; }
    public DateTime DateOfInquiring { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Package Package { get; set; } = null!;
    public Address SourceAddress { get; set; } = null!;
    public Address DestinationAddress { get; set; } = null!;
    public bool IsCompany { get; set; } // ?
    public bool HighPriority {  get; set; }
    public bool DeliveryAtWeekend { get; set; }
    // some status  
    public InquiryStatus Status { get; set; }
    public int? OfferId { get; set; }
    public string? CourierCompanyName { get; set; }
    // offer id, courier company name 
}
