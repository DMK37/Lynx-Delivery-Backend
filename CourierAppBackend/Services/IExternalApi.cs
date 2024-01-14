using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;

namespace CourierAppBackend.Services
{
    public interface IExternalApi
    {
        Task<OfferInfo> GetOffer(Inquiry inquiry);
        Task<string> GetToken();
    }
}