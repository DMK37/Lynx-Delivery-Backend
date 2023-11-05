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
}