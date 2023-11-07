using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;

namespace CourierAppBackend.Abstractions
{
    public interface IOffersRepository
    {
        Offer CreateNewOffer(CreateOffer createOffer);
        Offer GetOfferById(int ID);
    }
}
