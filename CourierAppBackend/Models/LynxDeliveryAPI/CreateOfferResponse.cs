using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Models.LynxDeliveryAPI;
public class CreateOfferResponse
{
    public int OfferId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public List<PriceItemDTO> Price { get; set; } = null!;
}
