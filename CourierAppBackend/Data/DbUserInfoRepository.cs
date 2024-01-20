using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbUserInfoRepository : IUserInfoRepository
{
    private readonly CourierAppContext _context;
    private readonly IAddressesRepository _addressesRepository;

    public DbUserInfoRepository(CourierAppContext context, IAddressesRepository addressesRepository)
    {
        _context = context;
        _addressesRepository = addressesRepository;
    }


    public async Task<UserInfo?> GetUserInfoById(string id)
    {
        var user = await (from u in _context.UsersInfos
            where u.UserId == id
            select u).Include(u => u.Address)
            .Include(u => u.DefaultSourceAddress).FirstOrDefaultAsync();
        return user;
    }

    public async Task<UserInfo> CreateUserInfo(UserInfo userInfo)
    {
        userInfo.Address = await _addressesRepository.FindOrAddAddress(userInfo.Address);
        userInfo.DefaultSourceAddress = await _addressesRepository.FindOrAddAddress(userInfo.DefaultSourceAddress);
        var usrInfo = await (from u in _context.UsersInfos
            where u.UserId == userInfo.UserId
            select u).FirstOrDefaultAsync();
        if (usrInfo == null)
        {
            await _context.UsersInfos.AddAsync(userInfo);
        }
        else
        {
            usrInfo.Address = userInfo.Address;
            usrInfo.DefaultSourceAddress = userInfo.DefaultSourceAddress;
            usrInfo.FirstName = userInfo.FirstName;
            usrInfo.LastName = userInfo.LastName;
        }

        await _context.SaveChangesAsync();
        return userInfo;
    }
}