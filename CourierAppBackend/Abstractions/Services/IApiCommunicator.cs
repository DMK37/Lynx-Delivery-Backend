using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Services;

public interface IApiCommunicator
{
    Task<TemporaryOffer> GetOffer(Inquiry inquiry);
    Task<string> GetToken();
}