namespace CourierAppBackend.Models.LecturerAPI;

public class CreateOfferRequestLecturer
{
    public string InquiryId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AddressLecturer Address { get; set; } = null!;
}
