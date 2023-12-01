using CourierAppBackend.Models;

namespace CourierAppBackend.ModelsDTO;

public class OrderU
{
    public OrderStatus OrderStatus { get; set; }
    public string? Comment { get; set; }
    public string CourierName { get; set; } = null!;
}
