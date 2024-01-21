using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Abstractions.Repositories;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using CourierAppBackend.Abstractions.Services;

namespace CourierAppBackend.Services;

public class LynxDeliveryAPI(IOffersRepository offersRepository, Abstractions.Services.IMessageSender messageSender, IPriceCalculator priceCalculator) 
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
        var list = priceCalculator.CalculatePrice(inquiry).ToDto();
        return new TemporaryOffer()
        {
            OfferID = offer.Id.ToString(),
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

    public async Task<TemporaryOffer?> SelectOffer(TemporaryOffer tempOffer, CustomerInfoDTO customerInfoDTO)
    {
        int offerId = int.Parse(tempOffer.OfferID);
        var offer = await offersRepository.SelectOffers(new()
        {
            OfferId = offerId,
            CustomerInfo = customerInfoDTO,
        });
        if (offer is not null)
        {
            await messageSender.SendOfferSelectedMessage(offer);
            return tempOffer;
        }
        return null;
    }

    public async Task<OfferInfo?> GetOfferInfo(TemporaryOffer inquiry)
    {
        var offer = await offersRepository.GetOffer(int.Parse(inquiry.OfferID));
        return offer.ToInfo();
    }
}
