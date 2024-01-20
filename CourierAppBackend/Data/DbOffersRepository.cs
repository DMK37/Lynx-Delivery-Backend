using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LynxDeliveryAPI;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data
{
    public class DbOffersRepository(CourierAppContext context, IAddressesRepository addressesRepository,
        IInquiriesRepository inquiriesRepository) : IOffersRepository
    {
        private readonly CourierAppContext _context = context;
        private readonly IAddressesRepository _addressesRepository = addressesRepository;
        private readonly IInquiriesRepository _inquiriesRepository = inquiriesRepository;

        public async Task<Offer> CreateOffer(OfferC createOffer)
        {
            var source = await _addressesRepository.AddAddress(createOffer.SourceAddress);
            var destination = await _addressesRepository.AddAddress(createOffer.DestinationAddress);

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

        public async Task<List<Offer>> GetPendingOffers()
        {
            var res = await _context.Offers.Where(x => x.Status == OfferStatus.Pending).Include(x => x.Inquiry)
                .Include(x => x.Inquiry.SourceAddress)
                .Include(x => x.Inquiry.DestinationAddress)
                .ToListAsync();
            return res;
        }

        public async Task<Offer> SelectOffer(OfferSelect offerSelect)
        {
            var address = await _addressesRepository.AddAddress(offerSelect.CustomerInfo.Address);

            var offer = _context.Offers.FirstOrDefault(x => x.Id == offerSelect.OfferId);
            if (offer == null)
                return null!;
            offer.Status = OfferStatus.Pending;
            offer.UpdateDate = DateTime.UtcNow;
            offer.CustomerInfo = new()
            {
                CompanyName = offerSelect.CustomerInfo.CompanyName,
                FirstName = offerSelect.CustomerInfo.FirstName,
                LastName = offerSelect.CustomerInfo.LastName,
                Address = address,
                Email = offerSelect.CustomerInfo.Email,
            };

            await _context.SaveChangesAsync();

            return offer;
        }

        public async Task<Offer> ConfirmOffer(int id, ConfirmOfferRequest request)
        {
            var address = await _addressesRepository.AddAddress(request.CustomerInfo.Address);

            var offer = _context.Offers.FirstOrDefault(x => x.Id == id);
            if (offer == null)
                return null!;
            offer.Status = OfferStatus.Pending;
            offer.UpdateDate = DateTime.UtcNow;
            offer.CustomerInfo = new()
            {
                CompanyName = request.CustomerInfo.CompanyName,
                FirstName = request.CustomerInfo.FirstName,
                LastName = request.CustomerInfo.LastName,
                Address = address,
                Email = request.CustomerInfo.Email
            };

            await _context.SaveChangesAsync();

            return offer;
        }

        public async Task<List<OfferInfo>> GetOfferInfos(OfferAll createOffers, IEnumerable<IApiCommunicator> _externalApis)
        {
            var inquiry = await _context.Inquiries.Include(x => x.SourceAddress).Include(x => x.DestinationAddress).FirstOrDefaultAsync(x => x.Id == createOffers.InquiryID);
            if (inquiry is null)
                return new List<OfferInfo>();
            var externalApis = _externalApis.ToList();
            var tasks = new Task<OfferInfo>[externalApis.Count];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = externalApis[i].GetOffer(inquiry);
            }

            var offerInfos = new List<OfferInfo>();

            Task<OfferInfo> timeoutTask = FakeTask();

            while (tasks.Length > 0)
            {
                var completedTask = await Task.WhenAny(tasks.Concat(new[] { timeoutTask }));

                if (completedTask == timeoutTask)
                {
                    Console.WriteLine("Timeout reached. Not all requests completed.");
                    break;
                }

                tasks = tasks.Where(t => t != completedTask).ToArray();
                OfferInfo res = await completedTask;

                if (res is not null)
                {
                    offerInfos.Add(res);
                }
            }

            return offerInfos;
        }

        public async Task<OfferInfo> FakeTask()
        {
            await Task.Delay(30000);
            return null!;
        }
        public async Task<CreateOfferResponse> CreateOffer(CreateOfferRequest request)
        {
            var source = await _addressesRepository.AddAddress(request.SourceAddress);
            var destination = await _addressesRepository.AddAddress(request.DestinationAddress);

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

            var response = new CreateOfferResponse
            {
                OfferId = offer.Id,
                CreationDate = offer.CreationDate,
                ExpireDate = offer.ExpireDate,
                Price = calc.CalculatePriceIntoBreakdown(inquiry)
            };

            return response;
        }
    }

}

