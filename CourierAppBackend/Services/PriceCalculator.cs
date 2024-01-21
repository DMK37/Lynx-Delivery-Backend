using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Services;

public class PriceCalculator : IPriceCalculator
{
    private static readonly decimal WeightMultiplier = 1;
    private static readonly decimal SizeMultiplier = 0.001M;
    public Price CalculatePrice(Inquiry inquiry)
    {
        Price price = new()
        {
            BaseDeliveryPrice = 10,
            PriorityFee = inquiry.HighPriority ? 10 : 0,
            DeliveryAtWeekendFee = inquiry.DeliveryAtWeekend ? 10 : 0,
            WeightFee = Math.Round((decimal)inquiry.Package.Weight * WeightMultiplier, 2, MidpointRounding.ToPositiveInfinity),
            SizeFee = Math.Round(inquiry.Package.Height * inquiry.Package.Width * inquiry.Package.Length * SizeMultiplier, 2, MidpointRounding.ToPositiveInfinity)
        };
        price.FullPrice = price.BaseDeliveryPrice + price.PriorityFee + price.DeliveryAtWeekendFee + price.WeightFee + price.SizeFee;
        return price;
    }
}
