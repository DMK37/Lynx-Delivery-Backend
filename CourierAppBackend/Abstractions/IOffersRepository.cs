using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using CourierAppBackend.ModelsPublicDTO;

namespace CourierAppBackend.Abstractions
{
    public interface IOffersRepository
    {
        Task<Offer> CreateNewOffer(OfferC createOffer);
        Task<Offer> CreateOffferFromOurInquiry(OfferAll createOffers);
        Task<Offer> GetOfferById(int ID);

        Task<List<Offer>> GetOffers();

        Task<Offer> CreateOfferFromRequest(CreateOfferRequest request);
        Task<Offer> SelectOffer(OfferSelect offerSelect);
    }
}
