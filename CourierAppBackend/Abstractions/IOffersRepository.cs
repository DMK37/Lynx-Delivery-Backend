using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;

namespace CourierAppBackend.Abstractions
{
    public interface IOffersRepository
    {
        Task<Offer> CreateNewOffer(CreateOffer createOffer);
        Task<Offer> GetOfferById(int ID);
    }
}
