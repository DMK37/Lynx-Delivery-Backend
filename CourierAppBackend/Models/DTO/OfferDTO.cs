using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Models.DTO;

public class OfferDTO
{
    public int OfferId { get; set; }
    public InquiryDTO Inquiry { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public OfferStatus Status { get; set; }
    public string? ReasonOfRejection { get; set; }
    public Price Price { get; set; } = null!;
    public CustomerInfoDTO? CustomerInfo { get; set; }
    public int? OrderID { get; set; }
}
