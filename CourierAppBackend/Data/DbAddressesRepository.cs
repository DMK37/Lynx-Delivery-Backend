using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbAddressesRepository : IAddressesRepository
{
    private readonly CourierAppContext _context;
    public DbAddressesRepository(CourierAppContext context)
    {
        _context = context;
    }
    public async Task<Address?> FindAddress(AddressDTO address)
    {
        var result = await _context.Addresses.FirstOrDefaultAsync(x => x.City == address.City &&
                                        x.PostalCode == address.PostalCode &&
                                        x.Street == address.Street &&
                                        x.HouseNumber == address.HouseNumber &&
                                        x.ApartmentNumber == address.ApartmentNumber);
        return result;
    }
    public async Task<Address> AddAddress(AddressDTO address)
    {
        Address newAddress = new()
        {
            ApartmentNumber = address.ApartmentNumber,
            City = address.City,
            PostalCode = address.PostalCode,
            Street = address.Street,
            HouseNumber = address.HouseNumber,
        };
        await _context.Addresses.AddAsync(newAddress);
        await _context.SaveChangesAsync();

        return newAddress;
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