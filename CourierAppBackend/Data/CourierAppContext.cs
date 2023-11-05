using Microsoft.EntityFrameworkCore;
using CourierAppBackend.Models;
namespace CourierAppBackend.Data;

public class CourierAppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public CourierAppContext(DbContextOptions options)
        : base(options)
    {
    }
}