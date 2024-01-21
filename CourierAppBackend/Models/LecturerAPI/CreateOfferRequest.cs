namespace CourierAppBackend.Models.LecturerAPI;

public class CreateOfferRequest
{
    public string InquiryId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AddressLecturer Address { get; set; } = null!;
}
