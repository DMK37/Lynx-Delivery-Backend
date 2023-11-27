using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;

namespace CourierAppBackend.Abstractions;

public interface IInquiriesRepository
{
    Task<List<Inquiry>> GetLastInquiries(string userId);
    Task<Inquiry> GetInquiryById(int id);
    Task<List<Inquiry>> GetAll();
    Task<Inquiry> CreateInquiry(CreateInquiry inquiry);
}