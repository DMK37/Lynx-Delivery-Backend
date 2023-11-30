using CourierAppBackend.Models;

namespace CourierAppBackend.DtoModels;

public class CreateInquiry
{
    public string? UserId { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Package Package { get; set; } = null!;
    public Address SourceAddress { get; set; } = null!;
    public Address DestinationAddress { get; set; } = null!;
    public bool IsCompany { get; set; } // ?
    public bool HighPriority {  get; set; }
    public bool DeliveryAtWeekend { get; set; }
}