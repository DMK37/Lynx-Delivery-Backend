using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbCreationalService : IHostedService
{
    private readonly IDbContextFactory<CourierAppContext> _contextFactory;

    public DbCreationalService(IDbContextFactory<CourierAppContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    { 
        var context = await _contextFactory.CreateDbContextAsync();

        await context.Database.EnsureCreatedAsync();
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}