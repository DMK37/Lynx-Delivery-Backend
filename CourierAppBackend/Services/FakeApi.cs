using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Services
{
    public class FakeApi : IApiCommunicator
    {
        public string Company { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<TemporaryOffer> GetOffer(Inquiry inquiry)
        {
            await Task.Delay(3000);
            return new TemporaryOffer
            {
                Company = "FakeCompany",
                TotalPrice = 10.0M
            };
        }

        public Task<OfferInfo?> GetOfferInfo(TemporaryOffer inquiry)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetToken()
        {
            throw new NotImplementedException();
        }

        public Task<TemporaryOffer?> SelectOffer(string id, CustomerInfoDTO customerInfoDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TemporaryOffer?> SelectOffer(TemporaryOffer offer, CustomerInfoDTO customerInfoDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TemporaryOffer?> SelectOffer(string id, CustomerInfoDTO customerInfoDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TemporaryOffer?> SelectOffer(TemporaryOffer offer, CustomerInfoDTO customerInfoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
