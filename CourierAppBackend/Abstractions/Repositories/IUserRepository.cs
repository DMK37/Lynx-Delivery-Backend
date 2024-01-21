using CourierAppBackend.Models.DTO;
namespace CourierAppBackend.Abstractions.Repositories;

public interface IUserRepository
{
    Task<UserDTO?> GetUserById(string id);
    Task<UserDTO> EditUser(UserDTO userInfo);
}