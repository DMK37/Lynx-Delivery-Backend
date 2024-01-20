namespace CourierAppBackend.Models.DTO;

public class PriceItemDTO
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = null!;
    public string Description { get; set; } = null!;
}