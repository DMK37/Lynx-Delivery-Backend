using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace CourierAppBackend.Data;

public class DbInquiriesRepository(CourierAppContext context, IAddressesRepository addressesRepository)
    : IInquiriesRepository
{
    public async Task<Inquiry?> GetInquiry(int inquiryId)
    {
        return await context.Inquiries
                            .Include(x => x.SourceAddress)
                            .Include(x => x.DestinationAddress)
                            .FirstOrDefaultAsync(x => x.Id == inquiryId);
    }

    public async Task<InquiryDTO?> GetInquiryById(int id)
    {
        var inquiry = await GetInquiry(id);
        return inquiry?.ToDto();
    }

    public async Task<List<InquiryDTO>> GetLastInquiries(string userId)
    {
        return await context.Inquiries
                            .AsNoTracking()
                            .Where(x => x.UserId == userId &&
                            (DateTime.UtcNow - x.DateOfInquiring).Days < 30)
                            .Include(x => x.SourceAddress)
                            .Include(x => x.DestinationAddress)
                            .Select(x => x.ToDto())
                            .ToListAsync();
    }

    public async Task<List<InquiryDTO>> GetAll()
    {
        return await context.Inquiries
                            .AsNoTracking()
                            .Include(x => x.SourceAddress)
                            .Include(x => x.DestinationAddress)
                            .Select(x => x.ToDto())
                            .ToListAsync();
    }

    public async Task<InquiryDTO> CreateInquiry(InquiryCreate inquiryCreate)
    {
        var source = await addressesRepository.AddAddress(inquiryCreate.SourceAddress);
        var destination = await addressesRepository.AddAddress(inquiryCreate.DestinationAddress);
        Inquiry inquiry = inquiryCreate.FromDto();
        inquiry.SourceAddress = source;
        inquiry.DestinationAddress = destination;
        await context.AddAsync(inquiry);
        await context.SaveChangesAsync();
        return inquiry.ToDto();
    }

    public async Task<InquiryDTO?> UpdateInquiry(string userId, int inquiryId)
    {
        var inquiry = await GetInquiry(inquiryId);
        if (inquiry is null || inquiry.UserId is not null) 
            return null;
        inquiry.UserId = userId;
        await context.SaveChangesAsync();
        return inquiry.ToDto();
    }
}