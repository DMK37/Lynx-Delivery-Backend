using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;

namespace CourierAppBackend.Services
{
    public class FakeApi : IExternalApi
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
