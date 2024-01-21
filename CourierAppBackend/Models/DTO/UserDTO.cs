
namespace CourierAppBackend.Models.DTO;

public class UserDTO
{
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AddressDTO Address { get; set; } = null!;
    public AddressDTO DefaultSourceAddress { get; set; } = null!;
}
