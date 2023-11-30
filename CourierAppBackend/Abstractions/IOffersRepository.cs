using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;

namespace CourierAppBackend.Abstractions
{
    public interface IOffersRepository
    {
        Task<Offer> CreateNewOffer(CreateOffer createOffer);
        Task<Offer> CreateOffferFromOurInquiry(CreateAllOffers cretOffers);
        Task<Offer> GetOfferById(int ID);
    }
}
