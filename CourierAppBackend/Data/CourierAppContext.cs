using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class CourierAppContext: DbContext
{
    //Add DbSet for each model
    
    public CourierAppContext(DbContextOptions options)
        : base(options)
    {
    }
}