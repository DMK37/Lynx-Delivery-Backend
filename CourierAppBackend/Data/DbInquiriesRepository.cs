using CourierAppBackend.Abstractions;
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

    public Inquiry CreateInquiry(Inquiry inquiry)
    {
        if (inquiry is null)
            return inquiry!;

        inquiry.DateOfInquiring = DateTime.UtcNow;
        inquiry.SourceAddress = _addressesRepository.FindOrAddAddress(inquiry.SourceAddress);
        inquiry.DestinationAddress = _addressesRepository.FindOrAddAddress(inquiry.DestinationAddress);

        _context.Add(inquiry);
        _context.SaveChanges();
        return inquiry;
    }
}