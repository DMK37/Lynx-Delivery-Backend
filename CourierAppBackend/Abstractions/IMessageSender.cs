using CourierAppBackend.Models;

namespace CourierAppBackend.Abstractions
{
    public interface IMessageSender
    {
        Task SendOfferSelectedMessage(Offer offer);
        Task SendOrderCreatedMessage(Order order);
    }
}