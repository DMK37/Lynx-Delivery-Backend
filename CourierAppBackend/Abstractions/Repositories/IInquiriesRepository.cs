using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Abstractions.Repositories;

public interface IInquiriesRepository
{
    Task<List<InquiryDTO>> GetLastInquiries(string userId);
    Task<InquiryDTO?> GetInquiryById(int id);
    Task<List<InquiryDTO>> GetAll();
    Task<InquiryDTO> CreateInquiry(InquiryCreate inquiry);

}