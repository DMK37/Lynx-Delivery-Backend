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

    public async Task<TemporaryOffer> GetOffer(Inquiry inquiry)
    {
        var offer = await offersRepository.CreateOffferFromOurInquiry(inquiry.Id);
        if (offer is null)
            return null!;
        PriceCalculator calc = new();
        var list = calc.CalculatePriceIntoBreakdown(inquiry);
        return new TemporaryOffer()
        {
            OfferID = offer.Id,
            Company = "Lynx Delivery",
            Inquiry = inquiry,
            TotalPrice = list.Sum(x => x.Amount),
            ExpiringAt = offer.ExpireDate.ToUniversalTime(),
            Currency = "Pln",
            PriceItems = list.Select(x => new PriceItem()
            {
                Amount = x.Amount,
                Description = x.Description,
                Currency = x.Currency,
            }).ToList()
        };
    }
}
