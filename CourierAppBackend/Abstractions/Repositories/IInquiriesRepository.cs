using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IInquiriesRepository
{
    Task<List<Inquiry>> GetLastInquiries(string userId);
    Task<Inquiry> GetInquiryById(int id);
    Task<List<Inquiry>> GetAll();
    Task<Inquiry> CreateInquiry(InquiryC inquiry);
}