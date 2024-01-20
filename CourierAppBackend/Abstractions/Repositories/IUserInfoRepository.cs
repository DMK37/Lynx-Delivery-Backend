using CourierAppBackend.Models.Database;
namespace CourierAppBackend.Abstractions.Repositories;

public interface IUserInfoRepository
{
    Task<UserInfo?> GetUserInfoById(string id);
    Task<UserInfo> CreateUserInfo(UserInfo userInfo);
}