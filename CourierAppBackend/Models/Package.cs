using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Models;

[Owned]
public class Package
{
    public int Height { get; set; }
    public int Width {  get; set; }
    public int Length {  get; set; }
    public float Weight { get; set; }
}
