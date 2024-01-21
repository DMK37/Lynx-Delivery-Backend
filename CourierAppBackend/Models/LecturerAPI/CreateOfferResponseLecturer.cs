namespace CourierAppBackend.Models.LecturerAPI;

public class CreateOfferResponseLecturer
{
    public string OfferRequestId { get; set; } = null!;
    public DateTime ValidTo { get; set; }
}
