namespace CourierAppBackend.Models.LecturerAPI;

public class CreateOfferResponse
{
    public string OfferRequestId { get; set; } = null!;
    public DateTime ValidTo { get; set; }
}
