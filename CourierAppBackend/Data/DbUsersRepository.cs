using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbUsersRepository(CourierAppContext context, IAddressesRepository addressesRepository)
    : IUserRepository
{
    public async Task<UserInfo?> GetUserInfoById(string id)
    {
        return await context.UsersInfos
                            .Where(u => u.UserId == id)
                            .Include(u => u.Address)
                            .Include(u => u.DefaultSourceAddress)
                            .FirstOrDefaultAsync();
    }

    public async Task<UserDTO> EditUser(UserDTO userDTO)
    {
        var address = await addressesRepository.AddAddress(userDTO.Address);
        var defaultSourceAddress = await addressesRepository.AddAddress(userDTO.DefaultSourceAddress);
        var user = await GetUserInfoById(userDTO.UserId);
        if (user is null)
        {
            UserInfo newUser = new()
            {
                UserId = userDTO.UserId,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                CompanyName = userDTO.CompanyName,
                Email = userDTO.Email,
                Address = address,
                DefaultSourceAddress = defaultSourceAddress,
            };
            await context.UsersInfos.AddAsync(newUser);
            await context.SaveChangesAsync();
            return userDTO;
        }
        user.FirstName = userDTO.FirstName;
        user.LastName = userDTO.LastName;
        user.CompanyName = userDTO.CompanyName;
        user.Address = address;
        user.DefaultSourceAddress = defaultSourceAddress;
        await context.SaveChangesAsync();
        return userDTO;
    }

    public async Task<UserDTO?> GetUserById(string id)
    {
        var userInfo = await GetUserInfoById(id);
        if (userInfo is null)
            return null;
        UserDTO user = userInfo.ToDto();
        return user;
    }
}