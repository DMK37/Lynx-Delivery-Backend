using CourierAppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Abstractions
{
    public interface IAddressesRepository
    {
        Task<Address> FindOrAddAddress(Address address);
    }
}
