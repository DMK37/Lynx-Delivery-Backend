namespace CourierAppBackend.Models.LecturerAPI;

public class PriceBreakDownItem
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = null!;
    public string Description { get; set; } = null!;
}