using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using CourierAppBackend.ModelsPublicDTO;
using CourierAppBackend.Services;

namespace CourierAppBackend.Abstractions
{
    public interface IOffersRepository
    {
        Task<Offer> CreateNewOffer(OfferC createOffer);
        Task<Offer> CreateOffferFromOurInquiry(OfferAll createOffers);
        Task<Offer> GetOfferById(int ID);

        Task<List<Offer>> GetOffers();
        
        Task<List<Offer>> GetPendingOffers();

        Task<Offer> CreateOfferFromRequest(CreateOfferRequest request);
        Task<Offer> SelectOffer(OfferSelect offerSelect);
        Task<Offer> ConfirmOffer(int id, ConfirmOfferRequest request);

        Task<List<OfferInfo>> GetOfferInfos(OfferAll createOffers, IEnumerable<IExternalApi> externalApis);
        
        
    }
}
