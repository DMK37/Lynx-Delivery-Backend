namespace CourierAppBackend.Models.LecturerAPI;

public class LecturerCreateInquireResponse
{
    public string InquiryId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public string Currency { get; set; } = null!;
    public DateTime ExpiringAt { get; set; }
    public List<PriceBreakDownItem> PriceBreakDown { get; set; } = null!;
}