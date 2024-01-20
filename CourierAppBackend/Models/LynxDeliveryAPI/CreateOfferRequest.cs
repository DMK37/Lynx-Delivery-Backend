using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace CourierAppBackend.Models.LynxDeliveryAPI;

public class CreateOfferRequest
{

    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Package Package { get; set; } = null!;
    public AddressDTO SourceAddress { get; set; } = null!;
    public AddressDTO DestinationAddress { get; set; } = null!;
    public bool IsCompany { get; set; }
    public bool HighPriority { get; set; }
    public bool DeliveryAtWeekend { get; set; }
}
