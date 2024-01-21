using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IOrdersRepository
{
    Task<List<OrderDTO>> GetAll();
    Task<OrderDTO?> GetOrderById(int id);
    Task<OrderDTO?> UpdateOrder(int id, OrderUpdate orderU);
}
