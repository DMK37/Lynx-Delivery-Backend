using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IInquiriesRepository
{
    Task<List<InquiryDTO>> GetAll();
    Task<List<InquiryDTO>> GetLastInquiries(string userId);
    Task<InquiryDTO?> GetInquiryById(int id);
    Task<InquiryDTO> CreateInquiry(InquiryCreate inquiry);
    Task<InquiryDTO> UpdateInquiry(string userId, int inquiryId);
}