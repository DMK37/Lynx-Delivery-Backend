using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace CourierAppBackend.Models;

[Owned]
public class CustomerInfo
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? CompanyName {  get; set; } = null!;
}
