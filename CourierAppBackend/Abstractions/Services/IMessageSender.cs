using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Services;

public interface IMessageSender
{
    Task SendOfferSelectedMessage(Offer offer);
    Task SendOrderCreatedMessage(OrderDTO order);
}