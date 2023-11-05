
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Models;

[Owned]
public class Price
{
    public float FullPrice { get; set; }
    public float Taxes { get; set; }
    public float Fees { get; set; }
    public float Value { get; set; }
}
