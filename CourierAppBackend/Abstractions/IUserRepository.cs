using CourierAppBackend.Models;
namespace CourierAppBackend.Abstractions;

public interface IUserRepository
{   
    User GetById(int id);
    void Add(User user);
}