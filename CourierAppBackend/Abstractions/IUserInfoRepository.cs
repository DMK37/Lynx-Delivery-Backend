using CourierAppBackend.Models;
namespace CourierAppBackend.Abstractions;

public interface IUserInfoRepository
{   
    Task<UserInfo?> GetUserInfoById(string id);
    Task<UserInfo> CreateUserInfo(UserInfo userInfo);
}