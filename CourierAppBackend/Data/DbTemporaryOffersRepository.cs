using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbTemporaryOffersRepository(CourierAppContext context)
    : ITemporaryOffersRepository
{
    public async Task<List<TemporaryOfferDTO>> GetExistingOffers(int inquiryId)
    {
        return await context.TemporaryOffers
                               .AsNoTracking()
                               .Include(x => x.Inquiry)
                               .Include(x => x.PriceItems)
                               .Where(x => x.Inquiry.Id == inquiryId)
                               .Select(x => x.ToDTO())
                               .ToListAsync();
    }
}
