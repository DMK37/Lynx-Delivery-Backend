using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Models.DTO;

public class OfferInfo
{
    public string OfferId { get; set; } = null!;
    public Package Package{ get; set; } = null!;
    public AddressDTO Source { get; set; } = null!;
    public AddressDTO Destination { get; set; } = null!;
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool DeliveryInWeekend { get; set; }
    public bool HighPriority { get; set; }
    public List<PriceItemDTO> PriceItems { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public string OfferStatus { get; set; } = null!;
    public string BuyerName { get; set; } = null!;
    public AddressDTO BuyerAddress { get; set; } = null!;
}
