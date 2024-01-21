using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbOrdersRepository(CourierAppContext context) 
    : IOrdersRepository
{
    public async Task<Order?> GetOrder(int id)
    {
        return await context.Orders
                            .Include(x => x.Offer)
                            .Include(x => x.Offer.CustomerInfo)
                            .Include(x => x.Offer.CustomerInfo!.Address)
                            .Include(x => x.Offer.Inquiry)
                            .Include(x => x.Offer.Inquiry.SourceAddress)
                            .Include(x => x.Offer.Inquiry.DestinationAddress)
                            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<OrderDTO>> GetAll()
    {
        return await context.Orders
                            .AsNoTracking()
                            .Include(x => x.Offer)
                            .Include(x => x.Offer.CustomerInfo)
                            .Include(x => x.Offer.CustomerInfo!.Address)
                            .Include(x => x.Offer.Inquiry)
                            .Include(x => x.Offer.Inquiry.SourceAddress)
                            .Include(x => x.Offer.Inquiry.DestinationAddress)
                            .Select(x => x.ToDTO())
                            .ToListAsync();
    }

    public async Task<OrderDTO?> GetOrderById(int id)
    {
        var order = await GetOrder(id);
        return order?.ToDTO();
    }

    public async Task<OrderDTO?> UpdateOrder(int id, OrderUpdate orderUpdate)
    {
        var order = await context.Orders.FindAsync(id);
        if (order is null)
            return null;
        order.OrderStatus = orderUpdate.OrderStatus;
        order.CourierName = orderUpdate.CourierName;
        order.LastUpdate = DateTime.UtcNow;
        order.Comment = orderUpdate.Comment;
        await context.SaveChangesAsync();
        return order.ToDTO();

    }

    public async Task<OrderDTO?> GetOrderByOfferId(int id)
    {
        var order = await context.Orders
                            .AsNoTracking()
                            .Include(x => x.Offer)
                            .Include(x => x.Offer.CustomerInfo)
                            .ThenInclude(x => x!.Address)
                            .FirstOrDefaultAsync(x => x.Offer.Id == id);
        return order?.ToDTO();
    }
}
