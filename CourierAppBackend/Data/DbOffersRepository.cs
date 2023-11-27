using CourierAppBackend.Abstractions;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data
{
    public class DbOffersRepository : IOffersRepository
    {
        private readonly CourierAppContext _context;
        private readonly IAddressesRepository _addressesRepository;
        public DbOffersRepository(CourierAppContext context, IAddressesRepository addressesRepository)
        {
            _context = context;
            _addressesRepository = addressesRepository;
        }

        public async Task<Offer> CreateNewOffer(CreateOffer createOffer)
        {
            Address source = new()
            {
                City = createOffer.SourceAddress.City,
                PostalCode = createOffer.SourceAddress.PostalCode,
                Street = createOffer.SourceAddress.Street,
                HouseNumber = createOffer.SourceAddress.HouseNumber,
                ApartmentNumber = createOffer.SourceAddress.ApartmentNumber

            };
            Address destination = new()
            {
                City = createOffer.DestinationAddress.City,
                PostalCode = createOffer.DestinationAddress.PostalCode,
                Street = createOffer.DestinationAddress.Street,
                HouseNumber = createOffer.DestinationAddress.HouseNumber,
                ApartmentNumber = createOffer.DestinationAddress.ApartmentNumber
            };
            source = await _addressesRepository.FindOrAddAddress(source);
            destination = await _addressesRepository.FindOrAddAddress(destination);
            // Calculate price for a package
            Price price = new()
            {
                Fees = 10,
                FullPrice = 30,
                Taxes = 10,
                Value = 10
            };

            Offer offer = new()
            {
                InquiryID = createOffer.InquiryID,
                CreationDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                PickupDate = createOffer.PickupDate,
                DeliveryDate = createOffer.DeliveryDate,
                Package = createOffer.Package,
                SourceAddress = source,
                DestinationAddress = destination,
                DeliveryAtWeekend = createOffer.DeliveryAtWeekend,
                HighPriority = createOffer.HighPriority,
                Status = OfferStatus.Offered,
                Price = price
            };

            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<Offer> GetOfferById(int ID)
        {
            var result = await _context.Offers
            .Include(x => x.SourceAddress)
            .Include(x => x.DestinationAddress)
            .FirstOrDefaultAsync(x => x.Id == ID);
            return result!;
        }
    }
}
