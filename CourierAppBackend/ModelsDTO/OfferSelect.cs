namespace CourierAppBackend.ModelsDTO;

public class OfferSelect
{
    public int OfferId { get; set; }
    public CustomerInfoC CustomerInfo { get; set; } = null!;
}
