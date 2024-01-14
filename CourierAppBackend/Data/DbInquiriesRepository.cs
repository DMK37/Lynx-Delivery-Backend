using CourierAppBackend.Abstractions;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbInquiriesRepository : IInquiriesRepository
{
    private readonly CourierAppContext _context;
    private readonly IAddressesRepository _addressesRepository;
    public DbInquiriesRepository(CourierAppContext context, IAddressesRepository addressesRepository)
    {
        _context = context;
        _addressesRepository = addressesRepository;
    }

    public async Task<List<Inquiry>> GetLastInquiries(string userId)
    {
        var inquiries = await (from i in _context.Inquiries
                         where i.UserId == userId && (DateTime.UtcNow - i.DateOfInquiring).Days < 30
                         select i)
            .Include(i => i.SourceAddress)
            .Include(i => i.DestinationAddress)
            .ToListAsync();
        return inquiries;
    }

    public async Task<List<Inquiry>> GetAll()
    {
        return await _context.Inquiries
            .Include(x => x.SourceAddress)
            .Include(x => x.DestinationAddress)
            .ToListAsync();
    }
    public async Task<Inquiry> GetInquiryById(int id)
    {
        var result = await _context.Inquiries
            .Include(x => x.SourceAddress)
            .Include(x => x.DestinationAddress)
            .FirstOrDefaultAsync(x => x.Id == id);
        return result!;
    }

    public async Task<Inquiry> CreateInquiry(InquiryC inquiryC)
    {
        var source = await _addressesRepository.FindAddress(inquiryC.SourceAddress);
        source ??= await _addressesRepository.AddAddress(inquiryC.SourceAddress);
        var destination = await _addressesRepository.FindAddress(inquiryC.DestinationAddress);
        destination ??= await _addressesRepository.AddAddress(inquiryC.DestinationAddress);

        Inquiry inquiry = new()
        {
            DateOfInquiring = DateTime.UtcNow,
            SourceAddress = source,
            DestinationAddress = destination,
            Package = inquiryC.Package,
            IsCompany = inquiryC.IsCompany,
            HighPriority = inquiryC.HighPriority,
            DeliveryAtWeekend = inquiryC.DeliveryAtWeekend,
            PickupDate = inquiryC.PickupDate,
            DeliveryDate = inquiryC.DeliveryDate,
            UserId = inquiryC.UserId,
            CourierCompanyName = "LynxDelivery"        
        };

        await _context.AddAsync(inquiry);
        await _context.SaveChangesAsync();
        return inquiry;
    }
}