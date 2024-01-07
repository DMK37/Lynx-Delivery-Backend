
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Models;

[Owned]
public class Price
{
    public decimal FullPrice { get; set; }
    public decimal BaseDeliveryPrice { get; set; }
    public decimal WeightFee { get; set; }
    public decimal SizeFee { get; set; }
    public decimal PriorityFee { get; set; }
    public decimal DeliveryAtWeekendFee { get; set; }
    public string Currency { get; set; } = "PLN";
}
