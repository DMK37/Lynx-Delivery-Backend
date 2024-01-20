using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
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
                                     // TODO
                                     .Select(x => x.ToDto())
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
                            // TODO
                            .Select(x => x.ToDto())
                            .ToListAsync();
    }

    public async Task<InquiryDTO> CreateInquiry(InquiryCreate inquiryCreate)
    {
        var source = await addressesRepository.AddAddress(inquiryCreate.SourceAddress);
        var destination = await addressesRepository.AddAddress(inquiryCreate.DestinationAddress);
        // TODO
        Inquiry inquiry = inquiryCreate.FromDto();
        inquiry.SourceAddress = source;
        inquiry.DestinationAddress = destination;
        await context.AddAsync(inquiry);
        await context.SaveChangesAsync();
        // TODO
        InquiryDTO inquiryDTO = inquiry.ToDto();
        return inquiryDTO;
    }

    public async Task<InquiryDTO?> GetInquiryById(int id)
    {
        var inquiry = await context.Inquiries
                                   .Include(x => x.SourceAddress)
                                   .Include(x => x.DestinationAddress)
                                   .Include(x => x.Package)
                                   .FirstOrDefaultAsync(x => x.Id == id);
        if (inquiry is null)
            return null;
        // TODO
        InquiryDTO inquiryDTO = inquiry.ToDto();
        return inquiryDTO;
    }
}