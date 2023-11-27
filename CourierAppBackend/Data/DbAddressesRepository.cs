
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
    public async Task<Address> FindOrAddAddress(Address address)
    {
        var resultAddress = await _context.Addresses.FirstOrDefaultAsync(x => x.City == address.City &&
                                        x.PostalCode == address.PostalCode &&
                                        x.Street == address.Street &&
                                        x.HouseNumber == address.HouseNumber &&
                                        x.ApartmentNumber == address.ApartmentNumber);
        if (resultAddress is null)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            resultAddress = address;
        }

        return resultAddress;
    }
}