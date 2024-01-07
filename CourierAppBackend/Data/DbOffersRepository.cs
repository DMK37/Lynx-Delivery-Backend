using CourierAppBackend.Abstractions;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using CourierAppBackend.ModelsPublicDTO;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data
{
    public class DbOffersRepository : IOffersRepository
    {
        private readonly CourierAppContext _context;
        private readonly IAddressesRepository _addressesRepository;
        private readonly IInquiriesRepository _inquiriesRepository;

        public DbOffersRepository(CourierAppContext context, IAddressesRepository addressesRepository,
            IInquiriesRepository inquiriesRepository)
        {
            _context = context;
            _addressesRepository = addressesRepository;
            _inquiriesRepository = inquiriesRepository;
        }

        public async Task<Offer> CreateNewOffer(OfferC createOffer)
        {
            var source = await _addressesRepository.FindAddress(createOffer.SourceAddress);
            source ??= await _addressesRepository.AddAddress(createOffer.SourceAddress);

            var destination = await _addressesRepository.FindAddress(createOffer.DestinationAddress);
            destination ??= await _addressesRepository.AddAddress(createOffer.DestinationAddress);

            Inquiry inquiry = new()
            {
                DateOfInquiring = DateTime.UtcNow,
                PickupDate = createOffer.PickupDate,
                DeliveryDate = createOffer.DeliveryDate,
                Package = createOffer.Package,
                SourceAddress = source,
                DestinationAddress = destination,
                IsCompany = createOffer.IsCompany,
                HighPriority = createOffer.HighPriority,
                DeliveryAtWeekend = createOffer.DeliveryAtWeekend,
                Status = InquiryStatus.Created,
                CourierCompanyName = "TODO"
            };

            await _context.Inquiries.AddAsync(inquiry);
            await _context.SaveChangesAsync();

            var calc = new PriceCalculator();
            var price = calc.CalculatePrice(inquiry);

            Offer offer = new()
            {
                Inquiry = inquiry,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMinutes(15),
                UpdateDate = DateTime.UtcNow,
                Status = OfferStatus.Offered,
                Price = price
            };

            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();

            return offer;
        }

        public async Task<Offer> CreateOffferFromOurInquiry(OfferAll createOffers)
        {
            var inquiry = await _context.Inquiries.FindAsync(createOffers.InquiryID);
            if (inquiry is null)
                return null!;

            //also need to calulate price
            var calc = new PriceCalculator();
            var price = calc.CalculatePrice(inquiry);

            Offer offer = new()
            {
                Inquiry = inquiry,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMinutes(15),
                UpdateDate = DateTime.UtcNow,
                Status = OfferStatus.Offered,
                Price = price
            };

            await _context.AddAsync(offer);
            await _context.SaveChangesAsync();

            return offer;
        }

        public async Task<Offer> GetOfferById(int ID)
        {
            var result = await _context.Offers
                .Include(x => x.Inquiry)
                .Include(x => x.Inquiry.SourceAddress)
                .Include(x => x.Inquiry.DestinationAddress)
                .FirstOrDefaultAsync(x => x.Id == ID);
            return result!;
        }

        public async Task<List<Offer>> GetOffers()
        {
            var res = await _context.Offers
                .Include(x => x.Inquiry)
                .Include(x => x.Inquiry.SourceAddress)
                .Include(x => x.Inquiry.DestinationAddress)
                .ToListAsync();
            return res;
        }

        public async Task<Offer> CreateOfferFromRequest(CreateOfferRequest request)
        {
            var source = await _addressesRepository.FindAddress(request.SourceAddress);
            source ??= await _addressesRepository.AddAddress(request.SourceAddress);

            var destination = await _addressesRepository.FindAddress(request.DestinationAddress);
            destination ??= await _addressesRepository.AddAddress(request.DestinationAddress);

            Inquiry inquiry = new()
            {
                DateOfInquiring = DateTime.UtcNow,
                PickupDate = request.PickupDate,
                DeliveryDate = request.DeliveryDate,
                Package = request.Package,
                SourceAddress = source,
                DestinationAddress = destination,
                IsCompany = request.IsCompany,
                HighPriority = request.HighPriority,
                DeliveryAtWeekend = request.DeliveryAtWeekend,
                Status = InquiryStatus.Created,
                CourierCompanyName = "TODO"
            };

            await _context.Inquiries.AddAsync(inquiry);
            await _context.SaveChangesAsync();

            var calc = new PriceCalculator();
            var price = calc.CalculatePrice(inquiry);

            Offer offer = new()
            {
                Inquiry = inquiry,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMinutes(15),
                UpdateDate = DateTime.UtcNow,
                Status = OfferStatus.Offered,
                Price = price
            };

            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();

            return offer;
        }
    }
}
