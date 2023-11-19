
using System.ComponentModel.DataAnnotations;

namespace CourierAppBackend.Models;

public class UserInfo
{
    [Key] public string UserId { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string Email { get; set; } = null!;
    public Address? Address { get; set; }
    public Address? DefaultSourceAddress { get; set; }
}
