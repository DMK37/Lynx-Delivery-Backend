using CourierAppBackend.Models;
using CourierAppBackend.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Abstractions
{
    public interface IAddressesRepository
    {
        Task<Address?> FindAddress(AddressDTO address);
        Task<Address> AddAddress(AddressDTO address);
        Task<Address> FindOrAddAddress(Address address);
    }
}
