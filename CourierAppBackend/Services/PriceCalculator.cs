using CourierAppBackend.Models;
using CourierAppBackend.ModelsLecturerApi;

namespace CourierAppBackend.Services;

public class PriceCalculator
{
    private static decimal weightMultiplier = 1;
    private static decimal sizeMultiplier = 0.0001M;
    public PriceCalculator() { }
    public Price CalculatePrice(Inquiry inquiry)
    {
        Price price = new()
        {
            BaseDeliveryPrice = 10,
            PriorityFee = inquiry.HighPriority ? 10 : 0,
            DeliveryAtWeekendFee = inquiry.DeliveryAtWeekend ? 10 : 0,
            WeightFee = Math.Round((decimal)inquiry.Package.Weight * weightMultiplier, 2, MidpointRounding.ToPositiveInfinity),
            SizeFee = Math.Round(inquiry.Package.Height * inquiry.Package.Width * inquiry.Package.Length * sizeMultiplier, 2, MidpointRounding.ToPositiveInfinity)
        };
        price.FullPrice = price.BaseDeliveryPrice + price.PriorityFee + price.DeliveryAtWeekendFee + price.WeightFee + price.SizeFee;
        return price;
    }
    public List<PriceBreakDownItem> CalculatePriceIntoBreakdown(Inquiry inquiry)
    {
        Price price = new()
        {
            BaseDeliveryPrice = 10,
            PriorityFee = inquiry.HighPriority ? 10 : 0,
            DeliveryAtWeekendFee = inquiry.DeliveryAtWeekend ? 10 : 0,
            WeightFee = Math.Round((decimal)inquiry.Package.Weight * weightMultiplier, 2, MidpointRounding.ToPositiveInfinity),
            SizeFee = Math.Round(inquiry.Package.Height * inquiry.Package.Width * inquiry.Package.Length * sizeMultiplier, 2, MidpointRounding.ToPositiveInfinity)
        };
        price.FullPrice = price.BaseDeliveryPrice + price.PriorityFee + price.DeliveryAtWeekendFee + price.WeightFee + price.SizeFee;
        List<PriceBreakDownItem> result = new();
        result.Add(new()
        {
            Amount = price.BaseDeliveryPrice,
            Currency = "Pln",
            Description = "Base"
        });
        result.Add(new()
        {
            Amount = price.PriorityFee,
            Currency = "Pln",
            Description = "Priority"
        });
        result.Add(new()
        {
            Amount = price.DeliveryAtWeekendFee,
            Currency = "Pln",
            Description = "Delivery at weekend"
        });
        result.Add(new()
        {
            Amount = price.WeightFee,
            Currency = "Pln",
            Description = "Weight"
        });
        result.Add(new()
        {
            Amount = price.SizeFee,
            Currency = "Pln",
            Description = "Size"
        });
        return result;
    }
}
