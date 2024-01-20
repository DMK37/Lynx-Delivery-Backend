using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Models.LecturerAPI;

public class CreateInquireResponse
{
    public string InquiryId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = null!;
    public DateTime ExpiringAt { get; set; }
    public List<PriceItemDTO> PriceBreakDown { get; set; } = null!;
}