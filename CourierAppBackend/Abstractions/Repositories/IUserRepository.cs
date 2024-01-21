using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IUserRepository
{
    Task<UserDTO?> GetUserById(string userId);
    Task<UserDTO> EditUser(UserDTO userDTO);
}