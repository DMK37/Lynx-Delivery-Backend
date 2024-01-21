using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LynxDeliveryAPI;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IOrdersRepository
{
    Task<Order> CreateOrder(Offer offer);
    Task<List<OrderDTO>> GetAll();
    Task<OrderDTO?> GetOrderById(int id);
    Task<OrderDTO?> GetOrderByOfferId(int id);
    Task<OrderDTO?> UpdateOrder(int id, OrderUpdate orderU);
    Task<List<OrderDTO>> GetUserOrders(string userId);
    Task<GetOrderResponse?> GetOrderAPI(int orderId);
}
