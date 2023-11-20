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

    public List<Inquiry> GetLastInquiries(string userId)
    {
        var inquiries = (from i in _context.Inquiries
                         where i.UserId == userId && (DateTime.UtcNow - i.DateOfInquiring).Days < 30
                         select i)
            .Include(i => i.SourceAddress)
            .Include(i => i.DestinationAddress)
            .ToList();
        return inquiries;
    }

    public List<Inquiry> GetAll()
    {
        return _context.Inquiries
            .Include(x => x.SourceAddress)
            .Include(x => x.DestinationAddress)
            .ToList();
    }
    public Inquiry GetInquiryById(int id)
    {
        var result = _context.Inquiries
            .Include(x => x.SourceAddress)
            .Include(x => x.DestinationAddress)
            .FirstOrDefault(x => x.Id == id);
        return result!;
    }

    public Inquiry CreateInquiry(CreateInquiry inquiry)
    {
        /*if (inquiry is null)
            return inquiry!;*/
        Inquiry inq = new Inquiry
        {
            DateOfInquiring = DateTime.UtcNow,
            SourceAddress = _addressesRepository.FindOrAddAddress(inquiry.SourceAddress),
            DestinationAddress = _addressesRepository.FindOrAddAddress(inquiry.DestinationAddress),
            Package = new Package
            {
                Height = inquiry.Package.Height,
                Width = inquiry.Package.Width,
                Length = inquiry.Package.Length,
                Weight = inquiry.Package.Weight
            },
            IsCompany = inquiry.IsCompany,
            HighPriority = inquiry.HighPriority,
            DeliveryAtWeekend = inquiry.DeliveryAtWeekend,
            PickupDate = inquiry.PickupDate,
            DeliveryDate = inquiry.DeliveryDate,
            UserId = inquiry.UserId
        };
        //inquiry = DateTime.UtcNow;
        //inquiry.SourceAddress = _addressesRepository.FindOrAddAddress(inquiry.SourceAddress);
        inquiry.DestinationAddress = _addressesRepository.FindOrAddAddress(inquiry.DestinationAddress);

        _context.Add(inq);
        _context.SaveChanges();
        return inq;
    }
}