using CourierAppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Abstractions
{
    public interface IAddressesRepository
    {
        Address FindOrAddAddress(Address address);
    }
}
