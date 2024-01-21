namespace CourierAppBackend.Models.DTO;

public class OfferSelect
{
    public int OfferId { get; set; }
    public CustomerInfoDTO CustomerInfo { get; set; } = null!;
}
