using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Models.LynxDeliveryAPI;

public class GetOfferResponse
{
    public int OfferId { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Package Package { get; set; } = null!;
    public AddressDTO SourceAddress { get; set; } = null!;
    public AddressDTO DestinationAddress { get; set; } = null!;
    public bool IsCompany { get; set; }
    public bool HighPriority { get; set; }
    public bool DeliveryAtWeekend { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string Status { get; set; } = null!;
    public CustomerInfoDTO? CustomerInfo { get; set; }
    public string? ReasonOfRejection { get; set; }
    public decimal TotalPrice { get; set; }
    public List<PriceItemDTO> Price { get; set; } = null!;
    public int? OrderId { get; set; }
}
