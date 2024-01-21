using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Abstractions.Services;

public interface IPriceCalculator
{
    Price CalculatePrice(Inquiry inquiry); 
}
