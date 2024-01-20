using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbInquiriesRepository(CourierAppContext context, IAddressesRepository addressesRepository) : IInquiriesRepository
{
    public async Task<List<InquiryDTO>> GetLastInquiries(string userId)
    {
        var inquiries = await context.Inquiries
                                     .AsNoTracking()
                                     .Where(x => x.UserId == userId &&
                                     (DateTime.UtcNow - x.DateOfInquiring).Days < 30)
                                     .Include(x => x.SourceAddress)
                                     .Include(x => x.DestinationAddress)
                                     .Include(x => x.Package)
                                     .Select(inquiry =>
                                     new InquiryDTO()
                                     {
                                         Id = inquiry.Id,
                                         UserId = inquiry.UserId,
                                         DateOfInquiring = inquiry.DateOfInquiring,
                                         PickupDate = inquiry.PickupDate,
                                         DeliveryDate = inquiry.DeliveryDate,
                                         Package = inquiry.Package,
                                         SourceAddress = new()
                                         {
                                             City = inquiry.SourceAddress.City,
                                             PostalCode = inquiry.SourceAddress.PostalCode,
                                             Street = inquiry.SourceAddress.Street,
                                             HouseNumber = inquiry.SourceAddress.HouseNumber,
                                             ApartmentNumber = inquiry.SourceAddress.ApartmentNumber
                                         },
                                         DestinationAddress = new()
                                         {
                                             City = inquiry.DestinationAddress.City,
                                             PostalCode = inquiry.DestinationAddress.PostalCode,
                                             Street = inquiry.DestinationAddress.Street,
                                             HouseNumber = inquiry.DestinationAddress.HouseNumber,
                                             ApartmentNumber = inquiry.DestinationAddress.ApartmentNumber
                                         },
                                         IsCompany = inquiry.IsCompany,
                                         HighPriority = inquiry.HighPriority,
                                         DeliveryAtWeekend = inquiry.DeliveryAtWeekend,
                                         Status = inquiry.Status,
                                         Company = inquiry.CourierCompanyName!
                                     })
                                     .ToListAsync();
        return inquiries;
    }

    public async Task<List<InquiryDTO>> GetAll()
    {
        return await context.Inquiries
                            .AsNoTracking()
                            .Include(x => x.SourceAddress)
                            .Include(x => x.DestinationAddress)
                            .Include(x => x.Package)
                            .Select(inquiry =>
                            new InquiryDTO()
                            {
                                Id = inquiry.Id,
                                UserId = inquiry.UserId,
                                DateOfInquiring = inquiry.DateOfInquiring,
                                PickupDate = inquiry.PickupDate,
                                DeliveryDate = inquiry.DeliveryDate,
                                Package = inquiry.Package,
                                SourceAddress = new()
                                {
                                    City = inquiry.SourceAddress.City,
                                    PostalCode = inquiry.SourceAddress.PostalCode,
                                    Street = inquiry.SourceAddress.Street,
                                    HouseNumber = inquiry.SourceAddress.HouseNumber,
                                    ApartmentNumber = inquiry.SourceAddress.ApartmentNumber
                                },
                                DestinationAddress = new()
                                {
                                    City = inquiry.DestinationAddress.City,
                                    PostalCode = inquiry.DestinationAddress.PostalCode,
                                    Street = inquiry.DestinationAddress.Street,
                                    HouseNumber = inquiry.DestinationAddress.HouseNumber,
                                    ApartmentNumber = inquiry.DestinationAddress.ApartmentNumber
                                },
                                IsCompany = inquiry.IsCompany,
                                HighPriority = inquiry.HighPriority,
                                DeliveryAtWeekend = inquiry.DeliveryAtWeekend,
                                Status = inquiry.Status,
                                Company = inquiry.CourierCompanyName!
                            })
                            .ToListAsync();
    }

    public async Task<InquiryDTO> CreateInquiry(InquiryCreate inquiryCreate)
    {
        var source = await addressesRepository.AddAddress(inquiryCreate.SourceAddress);
        var destination = await addressesRepository.AddAddress(inquiryCreate.DestinationAddress);

        Inquiry inquiry = new()
        {
            DateOfInquiring = DateTime.UtcNow,
            SourceAddress = source,
            DestinationAddress = destination,
            Package = inquiryCreate.Package,
            IsCompany = inquiryCreate.IsCompany,
            HighPriority = inquiryCreate.HighPriority,
            DeliveryAtWeekend = inquiryCreate.DeliveryAtWeekend,
            PickupDate = inquiryCreate.PickupDate,
            DeliveryDate = inquiryCreate.DeliveryDate,
            UserId = inquiryCreate.UserId,
            CourierCompanyName = "Lynx Delivery"
        };

        await context.AddAsync(inquiry);
        await context.SaveChangesAsync();

        InquiryDTO inquiryDTO = new()
        {
            Id = inquiry.Id,
            UserId = inquiry.UserId,
            DateOfInquiring = inquiry.DateOfInquiring,
            PickupDate = inquiry.PickupDate,
            DeliveryDate = inquiry.DeliveryDate,
            Package = inquiry.Package,
            SourceAddress = new()
            {
                City = inquiry.SourceAddress.City,
                PostalCode = inquiry.SourceAddress.PostalCode,
                Street = inquiry.SourceAddress.Street,
                HouseNumber = inquiry.SourceAddress.HouseNumber,
                ApartmentNumber = inquiry.SourceAddress.ApartmentNumber
            },
            DestinationAddress = new()
            {
                City = inquiry.DestinationAddress.City,
                PostalCode = inquiry.DestinationAddress.PostalCode,
                Street = inquiry.DestinationAddress.Street,
                HouseNumber = inquiry.DestinationAddress.HouseNumber,
                ApartmentNumber = inquiry.DestinationAddress.ApartmentNumber
            },
            IsCompany = inquiry.IsCompany,
            HighPriority = inquiry.HighPriority,
            DeliveryAtWeekend = inquiry.DeliveryAtWeekend,
            Status = inquiry.Status,
            Company = inquiry.CourierCompanyName!
        };
        return inquiryDTO;
    }

    public async Task<InquiryDTO?> GetInquiryById(int id)
    {
        var inquiry = await context.Inquiries
                                   .Include(x => x.SourceAddress)
                                   .Include(x => x.DestinationAddress)
                                   .FirstOrDefaultAsync(x => x.Id == id);
        if (inquiry is null)
            return null;
        InquiryDTO inquiryDTO = new()
        {
            Id = inquiry.Id,
            UserId = inquiry.UserId,
            DateOfInquiring = inquiry.DateOfInquiring,
            PickupDate = inquiry.PickupDate,
            DeliveryDate = inquiry.DeliveryDate,
            Package = inquiry.Package,
            SourceAddress = new()
            {
                City = inquiry.SourceAddress.City,
                PostalCode = inquiry.SourceAddress.PostalCode,
                Street = inquiry.SourceAddress.Street,
                HouseNumber = inquiry.SourceAddress.HouseNumber,
                ApartmentNumber = inquiry.SourceAddress.ApartmentNumber
            },
            DestinationAddress = new()
            {
                City = inquiry.DestinationAddress.City,
                PostalCode = inquiry.DestinationAddress.PostalCode,
                Street = inquiry.DestinationAddress.Street,
                HouseNumber = inquiry.DestinationAddress.HouseNumber,
                ApartmentNumber = inquiry.DestinationAddress.ApartmentNumber
            },
            IsCompany = inquiry.IsCompany,
            HighPriority = inquiry.HighPriority,
            DeliveryAtWeekend = inquiry.DeliveryAtWeekend,
            Status = inquiry.Status,
            Company = inquiry.CourierCompanyName!
        };
        return inquiryDTO;
    }
}