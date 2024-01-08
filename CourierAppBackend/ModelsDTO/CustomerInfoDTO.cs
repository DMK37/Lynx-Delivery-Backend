using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;

namespace CourierAppBackend.ModelsDTO;

public class CustomerInfoDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public AddressDTO Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? CompanyName { get; set; } = null!;
}
