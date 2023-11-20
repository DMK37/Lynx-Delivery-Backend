using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;

namespace CourierAppBackend.Abstractions;

public interface IInquiriesRepository
{
    List<Inquiry> GetLastInquiries(string userId);
    Inquiry GetInquiryById(int id);
    List<Inquiry> GetAll();
    Inquiry CreateInquiry(CreateInquiry inquiry);
}