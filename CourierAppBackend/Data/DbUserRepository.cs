using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;

namespace CourierAppBackend.Data;
public class DbUserRepository : IUserRepository
{
    private readonly CourierAppContext context;

    public DbUserRepository(CourierAppContext context)
    {
        this.context = context;
    }

    public User? GetById(int id)
    {
        return context.Users.Find(id);
    }

    public void Add(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }
}