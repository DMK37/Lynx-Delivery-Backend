using CourierAppBackend.Models;
namespace CourierAppBackend.Abstractions;

public interface IUserInfoRepository
{   
    UserInfo? GetUserInfoById(string id);
    UserInfo Add(UserInfo userInfo);
}