using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IOrdersRepository
{
    Task<List<Order>> GetOrders();
    Task<Order?> GetOrderById(int id);
    Task<Order> CreateOrder(OrderC orderC);
    Task<Order?> UpdateOrder(int id, OrderU orderU);
}
