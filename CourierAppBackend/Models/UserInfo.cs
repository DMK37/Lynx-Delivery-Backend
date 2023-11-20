
using System.ComponentModel.DataAnnotations;

namespace CourierAppBackend.Models;

public class UserInfo
{
    [Key] public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public Address DefaultSourceAddress { get; set; } = null!;
}
