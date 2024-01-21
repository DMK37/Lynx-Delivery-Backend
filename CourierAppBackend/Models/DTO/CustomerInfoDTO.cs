
namespace CourierAppBackend.Models.DTO;

public class CustomerInfoDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public AddressDTO Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? CompanyName { get; set; } = null!;
}
