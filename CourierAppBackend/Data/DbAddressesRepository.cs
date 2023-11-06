
using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbAddressesRepository : IAddressesRepository
{
    private readonly CourierAppContext _context;
    public DbAddressesRepository(CourierAppContext context)
    {
        _context = context;
    }
    public Address FindOrAddAddress(Address address)
    {
        var resultAddress = _context.Addresses.FirstOrDefault(x => x.City == address.City &&
                                        x.PostalCode == address.PostalCode &&
                                        x.Street == address.Street &&
                                        x.HouseNumber == address.HouseNumber &&
                                        x.ApartmentNumber == address.HouseNumber);
        if (resultAddress is null)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
            resultAddress = address;
        }

        return resultAddress;
    }
}