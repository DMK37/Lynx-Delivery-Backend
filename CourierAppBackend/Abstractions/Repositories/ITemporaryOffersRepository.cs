using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface ITemporaryOffersRepository
{
    Task<List<TemporaryOfferDTO>> GetExistingOffers(int inquiryId);
}
