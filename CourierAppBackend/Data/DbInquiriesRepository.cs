using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbInquiriesRepository : IInquiriesRepository
{
    private readonly CourierAppContext _context;
    public DbInquiriesRepository(CourierAppContext context)
    {
        _context = context;
    }

    public List<Inquiry> GetLastInquiries(int userId)
    {
        var inquiries = (from i in _context.Inquiries
                         where i.User.ID == userId && (DateTime.UtcNow - i.DateOfInquiring).Days < 30
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
            .FirstOrDefault(x => x.ID == id);
        return result!;
    }

    // just to work, it will have to be fixed so it looks better 
    public Inquiry CreateInquiry(Inquiry inquiry)
    {
        if (inquiry is null)
            return inquiry!;
        var source = _context.Addresses.FirstOrDefault(x => x.City == inquiry.SourceAddress.City &&
                                        x.PostalCode == inquiry.SourceAddress.PostalCode &&
                                        x.Street == inquiry.SourceAddress.Street &&
                                        x.HouseNumber == inquiry.SourceAddress.HouseNumber &&
                                        x.ApartmentNumber == inquiry.SourceAddress.HouseNumber);
        if (source is not null)
            inquiry.SourceAddress = source;
        else
        {
            _context.Addresses.Add(inquiry.SourceAddress);
            _context.SaveChanges();
        }
        var destination = _context.Addresses.FirstOrDefault(x => x.City == inquiry.DestinationAddress.City &&
                                        x.PostalCode == inquiry.DestinationAddress.PostalCode &&
                                        x.Street == inquiry.DestinationAddress.Street &&
                                        x.HouseNumber == inquiry.DestinationAddress.HouseNumber &&
                                        x.ApartmentNumber == inquiry.DestinationAddress.ApartmentNumber);
        if (destination is not null)
            inquiry.DestinationAddress = destination;
        else
        {
            _context.Addresses.Add(inquiry.DestinationAddress);
            _context.SaveChanges();
        }
        _context.Add(inquiry);
        _context.SaveChanges();
        return inquiry;
    }
}