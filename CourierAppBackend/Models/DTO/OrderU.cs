using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Models.DTO;

public class OrderU
{
    public OrderStatus OrderStatus { get; set; }
    public string? Comment { get; set; }
    public string CourierName { get; set; } = null!;
}
