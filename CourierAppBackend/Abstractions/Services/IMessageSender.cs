using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Abstractions.Services
{
    public interface IMessageSender
    {
        Task SendOfferSelectedMessage(Offer offer);
        Task SendOrderCreatedMessage(Order order);
    }
}