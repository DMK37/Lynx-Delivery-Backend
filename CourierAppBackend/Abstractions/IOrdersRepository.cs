using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;

namespace CourierAppBackend.Abstractions;

public interface IOrdersRepository
{
    Task<List<Order>> GetOrders();
    Task<Order?> GetOrderById(int id);
    Task<Order> CreateOrder(OrderC orderC);
    Task<Order?> UpdateOrder(int id, OrderU orderU);
}
