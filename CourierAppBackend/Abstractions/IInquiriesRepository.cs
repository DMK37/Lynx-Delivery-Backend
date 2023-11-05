using CourierAppBackend.Models;

namespace CourierAppBackend.Abstractions;

public interface IInquiriesRepository
{
    List<Inquiry> GetLastInquiries(int userId);
}