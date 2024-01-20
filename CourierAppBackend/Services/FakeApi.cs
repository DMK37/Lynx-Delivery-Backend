using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Services
{
    public class FakeApi : IApiCommunicator
    {
        public async Task<OfferInfo> GetOffer(Inquiry inquiry)
        {
            await Task.Delay(3000);
            return new OfferInfo
            {
                Company = "FakeCompany",
                TotalPrice = 10.0M
            };
        }

        public async Task<string> GetToken()
        {
            throw new NotImplementedException();
        }
    }
}
