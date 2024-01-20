using Microsoft.EntityFrameworkCore;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Abstractions.Repositories
{
    public interface IAddressesRepository
    {
        Task<Address?> FindAddress(AddressDTO address);
        Task<Address> AddAddress(AddressDTO address);
        Task<Address> FindOrAddAddress(Address address);
    }
}
