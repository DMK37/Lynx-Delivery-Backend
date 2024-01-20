using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LynxDeliveryAPI;
using CourierAppBackend.Services;

namespace CourierAppBackend.Abstractions.Repositories
{
    public interface IOffersRepository
    {
        Task<Offer> CreateOffer(OfferC createOffer);
        Task<Offer> CreateOffferFromOurInquiry(OfferAll createOffers);
        Task<Offer> GetOfferById(int ID);

        Task<List<Offer>> GetOffers();

        Task<List<Offer>> GetPendingOffers();
        Task<Offer> SelectOffer(OfferSelect offerSelect);
        Task<Offer> ConfirmOffer(int id, ConfirmOfferRequest request);

        Task<List<OfferInfo>> GetOfferInfos(OfferAll createOffers, IEnumerable<IApiCommunicator> externalApis);
        // Create offer from request from other company
        Task<CreateOfferResponse> CreateOffer(CreateOfferRequest request);

    }
}
