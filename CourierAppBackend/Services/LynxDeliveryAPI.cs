using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Abstractions.Repositories;

namespace CourierAppBackend.Services;

public class LynxDeliveryAPI(IOffersRepository offersRepository) 
    : IApiCommunicator
{
    public string Company => "Lynx Delivery";
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

    public async Task<TemporaryOffer?> SelectOffer(string id, CustomerInfoDTO customerInfoDTO)
    {
        int offerId = int.Parse(id);
        await offersRepository.SelectOffers(new()
        {
            OfferId = offerId,
            CustomerInfo = customerInfoDTO,
        });
        throw new NotImplementedException();
    }

    public async Task<TemporaryOffer?> SelectOffer(TemporaryOffer tempOffer, CustomerInfoDTO customerInfoDTO)
    {
        int offerId = tempOffer.OfferID;
        var offer = await offersRepository.SelectOffers(new()
        {
            OfferId = offerId,
            CustomerInfo = customerInfoDTO,
        });
        if (offer is not null)
            return tempOffer;
        return null;
    }
}
