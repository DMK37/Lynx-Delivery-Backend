
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Models;

[Owned]
public class Price
{
    public decimal FullPrice { get; set; }
    public decimal Taxes { get; set; }
    public decimal Fees { get; set; }
    public decimal Value { get; set; }
}
