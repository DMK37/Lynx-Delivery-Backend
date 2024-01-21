using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LynxDeliveryAPI;
using CourierAppBackend.Services;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IOffersRepository
{
    Task<Offer?> GetOffer(int offerId);
    Task<OfferDTO?> GetOfferById(int offerId);
    Task<List<OfferDTO>> GetAll();
    Task<List<OfferDTO>> GetPending();
    Task<Offer?> CreateOffer(int inquiryId);
    Task<Offer?> SelectOffer(int offerRequestId, CustomerInfoDTO customerInfoDTO);
    Task<Offer> ConfirmOffer(int id, ConfirmOfferRequest request);

    Task<List<TemporaryOfferDTO>?> GetOffers(int inquiryId, List<IApiCommunicator> apis);
    Task<bool> SelectOffer(int id, CustomerInfoDTO customerInfoDTO, List<IApiCommunicator> apis);
    Task<OfferInfo?> GetOfferInfo(int id, List<IApiCommunicator> apis);
    // Create offer from request from other company
    Task<CreateOfferResponse> CreateOffer(CreateOfferRequest request);
    Task<bool> AcceptOffer(int id);
    Task<bool> RejectOffer(int id, string reason);


}
