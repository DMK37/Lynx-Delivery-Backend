using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Abstractions.Repositories;

namespace CourierAppBackend.Services;

public class LynxDeliveryAPI(IOffersRepository offersRepository) 
    : IApiCommunicator
{
    public Task<string> GetToken()
    {
        throw new NotImplementedException();
    }

    public async Task<OfferInfo> GetOffer(Inquiry inquiry)
    {
        var offer = await offersRepository.CreateOffferFromOurInquiry(new() { InquiryID = inquiry.Id });
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
