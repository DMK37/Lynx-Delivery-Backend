using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IInquiriesRepository
{
    Task<Inquiry?> GetInquiry(int inquiryId);
    Task<InquiryDTO?> GetInquiryById(int inquiryId);
    Task<List<InquiryDTO>> GetAll();
    Task<List<InquiryDTO>> GetLastInquiries(string userId);
    Task<InquiryDTO> CreateInquiry(InquiryCreate inquiryCreate);
    Task<InquiryDTO?> UpdateInquiry(string userId, int inquiryId);
}