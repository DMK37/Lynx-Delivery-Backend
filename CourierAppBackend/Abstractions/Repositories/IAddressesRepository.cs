using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IAddressesRepository
{
    Task<Address> AddAddress(AddressDTO address);
}
