using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Services;

public interface IApiCommunicator
{
    string Company { get; }
    Task<string> GetToken();
    Task<TemporaryOffer> GetOffer(Inquiry inquiry);
    Task<TemporaryOffer?> SelectOffer(TemporaryOffer offer, CustomerInfoDTO customerInfoDTO);
}