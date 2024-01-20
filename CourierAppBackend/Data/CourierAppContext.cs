using Microsoft.EntityFrameworkCore;
using CourierAppBackend.Models.Database;
namespace CourierAppBackend.Data;

public class CourierAppContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserInfo> UsersInfos { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
}