using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LynxDeliveryAPI;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbOffersRepository(CourierAppContext context, IAddressesRepository addressesRepository, 
    IOrdersRepository ordersRepository, IPriceCalculator priceCalculator, IInquiriesRepository inquiriesRepository,
    ITemporaryOffersRepository temporaryOffersRepository)
    : IOffersRepository
{
    public async Task<Offer?> GetOffer(int offerId)
    {
        return await context.Offers
                            .Include(x => x.Inquiry)
                            .Include(x => x.Inquiry.SourceAddress)
                            .Include(x => x.Inquiry.DestinationAddress)
                            .Include(x => x.CustomerInfo)
                            .ThenInclude(x => x!.Address)
                            .FirstOrDefaultAsync(x => x.Id == offerId);
    }

    public async Task<OfferDTO?> GetOfferById(int id)
    {
        var offer = await GetOffer(id);
        return offer?.ToDTO();
    }

    public async Task<List<OfferDTO>> GetAll()
    {
        return await context.Offers
                            .AsNoTracking()
                            .Include(x => x.Inquiry)
                            .Include(x => x.Inquiry.SourceAddress)
                            .Include(x => x.Inquiry.DestinationAddress)
                            .Include(x => x.CustomerInfo)
                            .ThenInclude(x => x!.Address)
                            .Select(x => x.ToDTO())
                            .ToListAsync();
    }

    public async Task<List<OfferDTO>> GetPending()
    {
        return await context.Offers
                            .AsNoTracking()
                            .Where(x => x.Status == OfferStatus.Pending)
                            .Include(x => x.Inquiry)
                            .Include(x => x.Inquiry.SourceAddress)
                            .Include(x => x.Inquiry.DestinationAddress)
                            .Include(x => x.CustomerInfo)
                            .ThenInclude(x => x!.Address)
                            .Select(x => x.ToDTO())
                            .ToListAsync();
    }

    public async Task<Offer?> CreateOffer(int inquiryId)
    {
        var inquiry = await inquiriesRepository.GetInquiry(inquiryId);
        if (inquiry is null)
            return null;
        Offer newOffer = new()
        {
            Inquiry = inquiry,
            CreationDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddHours(6),
            UpdateDate = DateTime.UtcNow,
            Status = OfferStatus.Offered,
            Price = priceCalculator.CalculatePrice(inquiry)
        };
        await context.AddAsync(newOffer);
        await context.SaveChangesAsync();
        return newOffer;
    }

    public async Task<Offer?> SelectOffer(int inquiryId, CustomerInfoDTO customerInfoDTO)
    {
        var address = await addressesRepository.AddAddress(customerInfoDTO.Address);
        var offer = await context.Offers
                                 .Include(x => x.Inquiry)
                                 .FirstOrDefaultAsync(x => x.Id == inquiryId);
        if (offer is null)
            return null;
        offer.Status = OfferStatus.Pending;
        offer.UpdateDate = DateTime.UtcNow;
        offer.CustomerInfo = customerInfoDTO.FromDTO();
        offer.CustomerInfo.Address = address;
        offer.Inquiry.OfferID = offer.Id;
        await context.SaveChangesAsync();
        return offer;
    }


    public async Task<List<TemporaryOfferDTO>?> GetOffers(int inquiryId, List<IApiCommunicator> apis)
    {
        var inquiry = await inquiriesRepository.GetInquiry(inquiryId);
        if (inquiry is null)
            return null;
        if (inquiry.Status == InquiryStatus.OffersRequested)
            return await temporaryOffersRepository.GetExistingOffers(inquiryId);

        var tasks = new List<Task<TemporaryOffer?>>();
        var offers = new List<TemporaryOfferDTO>();
        foreach (var api in apis)
            tasks.Add(api.GetOffer(inquiry));

        var timeoutTask = FakeTask();
        tasks.Add(timeoutTask);

        while (tasks.Count > 1)
        {
            var completedTask = await Task.WhenAny(tasks);
            if (completedTask == timeoutTask)
                break;
            tasks.Remove(completedTask);
            var result = await completedTask;
            if (result is not null)
            {
                await context.TemporaryOffers.AddAsync(result);
                await context.SaveChangesAsync();
                offers.Add(result.ToDTO());
            }
        }

        inquiry.Status = InquiryStatus.OffersRequested;
        await context.SaveChangesAsync();
        return offers;
    }

    public async Task<TemporaryOffer?> FakeTask()
    {
        await Task.Delay(30000);
        return null;
    }

    public async Task<bool> SelectOffer(int offerRequestId, CustomerInfoDTO customerInfoDTO, List<IApiCommunicator> apis)
    {
        var tempOffer = await context.TemporaryOffers
                                     .Include(x => x.Inquiry)
                                     .FirstOrDefaultAsync(x => x.Id == offerRequestId);
        if (tempOffer is null)
            return false;
        var api = apis.Find(x => x.Company == tempOffer.Company);
        if (api is null)
            return false;
        var offer = await api.SelectOffer(tempOffer, customerInfoDTO);
        if (offer is not null)
        {
            tempOffer.Inquiry.Status = InquiryStatus.Accepted;
            tempOffer.Inquiry.DeliveringCompany = tempOffer.Company;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> AcceptOffer(int id)
    {
        var offer = await GetOffer(id);
        if (offer is null)
            return false;
        Order order = new()
        {
            OfferID = offer.Id,
            Offer = offer,
            OrderStatus = OrderStatus.Accepted,
            LastUpdate = DateTime.UtcNow,
            CourierName = ""
        };
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        offer.UpdateDate = DateTime.UtcNow;
        offer.OrderID = order.Id;
        offer.Status = OfferStatus.Accepted;
        await context.SaveChangesAsync();
        return true;

    }

    public async Task<bool> RejectOffer(int id, string reason)
    {
        var offer = await GetOffer(id);
        if (offer is null)
            return false;
        offer.UpdateDate = DateTime.UtcNow;
        offer.ReasonOfRejection = reason;
        offer.Status = OfferStatus.Rejected;
        await context.SaveChangesAsync();
        return true;

    }

    public async Task<OfferInfo?> GetOfferInfo(int id, List<IApiCommunicator> apis)
    {
        var inquiry = await context.Inquiries
                                   .FindAsync(id);
        if (inquiry is null)
            return null;
        var tempOffer = await context.TemporaryOffers
                                     .FirstOrDefaultAsync(x =>
                                     x.Inquiry.Id == id &&
                                     x.Company == inquiry.DeliveringCompany);
        if (tempOffer is null)
            return null;
        var api = apis.Find(x => x.Company == tempOffer.Company);
        if (api is null)
            return null;
        var res = await api.GetOfferInfo(tempOffer);
        await context.SaveChangesAsync();
        return res;
    }
    public async Task<OrderDTO?> GetOrderId(int offerId)
    {
        var offer = await GetOffer(offerId);
        if (offer is null) return null;
        if (offer.OrderID is null) return null;
        return await ordersRepository.GetOrderById(offer.OrderID.Value);
    }

    public async Task<CreateOfferResponse> CreateOffer(CreateOfferRequest request)
    {
        var source = await addressesRepository.AddAddress(request.SourceAddress);
        var destination = await addressesRepository.AddAddress(request.DestinationAddress);

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

        await context.Inquiries.AddAsync(inquiry);
        await context.SaveChangesAsync();

        var price = priceCalculator.CalculatePrice(inquiry);

        Offer offer = new()
        {
            Inquiry = inquiry,
            CreationDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMinutes(15),
            UpdateDate = DateTime.UtcNow,
            Status = OfferStatus.Offered,
            Price = price
        };

        await context.Offers.AddAsync(offer);
        await context.SaveChangesAsync();

        var response = new CreateOfferResponse
        {
            OfferId = offer.Id,
            CreationDate = offer.CreationDate,
            ExpireDate = offer.ExpireDate,
            Price = priceCalculator.CalculatePrice(inquiry).ToDTO()
        };

        return response;
    }

    public async Task<Offer> ConfirmOffer(int id, ConfirmOfferRequest request)
    {
        var address = await addressesRepository.AddAddress(request.CustomerInfo.Address);

        var offer = await context.Offers.FirstOrDefaultAsync(x => x.Id == id);
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

        await context.SaveChangesAsync();

        return offer;
    }
}

