using CourierAppBackend.Models;

namespace CourierAppBackend.Abstractions;

public interface IInquiriesRepository
{
    List<Inquiry> GetLastInquiries(int userId);
    Inquiry GetInquiryById(int id);
    List<Inquiry> GetAll();
    Inquiry CreateInquiry(Inquiry inquiry);
}