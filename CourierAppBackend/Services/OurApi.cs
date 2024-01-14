using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using CourierAppBackend.Abstractions;
using CourierAppBackend.ModelsLecturerApi;

namespace CourierAppBackend.Services
{
    public class OurApi : IExternalApi
    {
        private readonly IOffersRepository _offersRepository;
        public OurApi(IOffersRepository offersRepository)
        {
            _offersRepository = offersRepository;
        }

        public async Task<string> GetToken()
        {
            throw new NotImplementedException();
        }

        public async Task<OfferInfo> GetOffer(Inquiry inquiry)
        {
            var offer = await _offersRepository.CreateOffferFromOurInquiry(new() { InquiryID = inquiry.Id });
            if (offer is null)
                return null!;
            PriceCalculator calc = new();
            var list = calc.CalculatePriceIntoBreakdown(inquiry);
            return new OfferInfo()
            {
                OfferId = offer.Id,
                Company = "Lynx Delivery",
                InquiryId = offer.Inquiry.Id.ToString(),
                TotalPrice = list.Sum(x => x.Amount),
                ExpiringAt = offer.ExpireDate,
                PriceBreakDown = list
            };
        }
    }

}
