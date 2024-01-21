using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LecturerAPI;

namespace CourierAppBackend.Services;

public static class Mapping
{
    public static AddressDTO ToDto(this LecturerAddress address)
    {
        return new()
        {
            Street = address.Street,
            HouseNumber = address.HouseNumber,
            ApartmentNumber = address.ApartmentNumber,
            City = address.City,
            PostalCode = address.ZipCode
        };
    }
    public static Address FromDto(this AddressDTO addressDTO)
    {
        return new()
        {
            ApartmentNumber = addressDTO.ApartmentNumber,
            City = addressDTO.City,
            PostalCode = addressDTO.PostalCode,
            Street = addressDTO.Street,
            HouseNumber = addressDTO.HouseNumber,
        };
    }
    public static AddressDTO ToDto(this Address address)
    {
        return new()
        {
            ApartmentNumber = address.ApartmentNumber,
            City = address.City,
            PostalCode = address.PostalCode,
            Street = address.Street,
            HouseNumber = address.HouseNumber
        };
    }
    public static CustomerInfoDTO ToDto(this CustomerInfo customerInfo)
    {
        return new()
        {
            FirstName = customerInfo.FirstName,
            LastName = customerInfo.LastName,
            CompanyName = customerInfo.CompanyName,
            Email = customerInfo.Email,
            Address = customerInfo.Address.ToDto()
        };
    }
    public static UserDTO ToDto(this UserInfo userInfo)
    {
        return new()
        {
            UserId = userInfo.UserId,
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            CompanyName = userInfo.CompanyName,
            Email = userInfo.Email,
            Address = new()
            {
                City = userInfo.Address.City,
                PostalCode = userInfo.Address.PostalCode,
                Street = userInfo.Address.Street,
                HouseNumber = userInfo.Address.HouseNumber,
                ApartmentNumber = userInfo.Address.ApartmentNumber
            },
            DefaultSourceAddress = new()
            {
                City = userInfo.DefaultSourceAddress.City,
                PostalCode = userInfo.DefaultSourceAddress.PostalCode,
                Street = userInfo.DefaultSourceAddress.Street,
                HouseNumber = userInfo.DefaultSourceAddress.HouseNumber,
                ApartmentNumber = userInfo.DefaultSourceAddress.ApartmentNumber
            }
        };
    }
    public static InquiryDTO ToDto(this Inquiry inquiry)
    {
        return new()
        {
            Id = inquiry.Id,
            UserId = inquiry.UserId,
            DateOfInquiring = inquiry.DateOfInquiring,
            PickupDate = inquiry.PickupDate,
            DeliveryDate = inquiry.DeliveryDate,
            Package = inquiry.Package,
            SourceAddress = new()
            {
                City = inquiry.SourceAddress.City,
                PostalCode = inquiry.SourceAddress.PostalCode,
                Street = inquiry.SourceAddress.Street,
                HouseNumber = inquiry.SourceAddress.HouseNumber,
                ApartmentNumber = inquiry.SourceAddress.ApartmentNumber
            },
            DestinationAddress = new()
            {
                City = inquiry.DestinationAddress.City,
                PostalCode = inquiry.DestinationAddress.PostalCode,
                Street = inquiry.DestinationAddress.Street,
                HouseNumber = inquiry.DestinationAddress.HouseNumber,
                ApartmentNumber = inquiry.DestinationAddress.ApartmentNumber
            },
            IsCompany = inquiry.IsCompany,
            HighPriority = inquiry.HighPriority,
            DeliveryAtWeekend = inquiry.DeliveryAtWeekend,
            Status = inquiry.Status,
            Company = inquiry.CourierCompanyName!
        };
    }
    public static Inquiry FromDto(this InquiryCreate inquiryCreate)
    {
        return new()
        {
            DateOfInquiring = DateTime.UtcNow,
            Package = inquiryCreate.Package,
            IsCompany = inquiryCreate.IsCompany,
            HighPriority = inquiryCreate.HighPriority,
            DeliveryAtWeekend = inquiryCreate.DeliveryAtWeekend,
            PickupDate = inquiryCreate.PickupDate,
            DeliveryDate = inquiryCreate.DeliveryDate,
            UserId = inquiryCreate.UserId,
            CourierCompanyName = "Lynx Delivery"
        };
    }
    public static CreateInquireRequest ToRequest(this Inquiry inquiry)
    {
        return new()
        {
            Dimensions = new()
            {
                Width = inquiry.Package.Width,
                Height = inquiry.Package.Height,
                Length = inquiry.Package.Length,
            },
            Weight = inquiry.Package.Weight,
            Source = new()
            {
                Street = inquiry.SourceAddress.Street,
                HouseNumber = inquiry.SourceAddress.HouseNumber,
                ApartmentNumber = inquiry.SourceAddress.ApartmentNumber,
                City = inquiry.SourceAddress.City,
                ZipCode = inquiry.SourceAddress.PostalCode
            },
            Destination = new()
            {
                Street = inquiry.DestinationAddress.Street,
                HouseNumber = inquiry.DestinationAddress.HouseNumber,
                ApartmentNumber = inquiry.DestinationAddress.ApartmentNumber,
                City = inquiry.DestinationAddress.City,
                ZipCode = inquiry.DestinationAddress.PostalCode
            },
            PickupDate = inquiry.PickupDate,
            DeliveryDay = inquiry.DeliveryDate,
            deliveryInWeekend = inquiry.DeliveryAtWeekend,
            Priority = inquiry.HighPriority ? "High" : "Low",
            isComapny = inquiry.IsCompany
        };
    }
    public static List<PriceItemDTO> ToDto(this Price price)
    {
        List<PriceItemDTO> result =
        [
            new()
            {
                Amount = price.BaseDeliveryPrice,
                Currency = "Pln",
                Description = "Base"
            },
            new()
            {
                Amount = price.PriorityFee,
                Currency = "Pln",
                Description = "Priority"
            },
            new()
            {
                Amount = price.DeliveryAtWeekendFee,
                Currency = "Pln",
                Description = "Delivery at weekend"
            },
            new()
            {
                Amount = price.WeightFee,
                Currency = "Pln",
                Description = "Weight"
            },
            new()
            {
                Amount = price.SizeFee,
                Currency = "Pln",
                Description = "Size"
            },
        ];
        result.RemoveAll(x => x.Amount == 0);
        return result;
    }
    public static OfferDTO ToDTO(this Offer offer)
    {
        return new()
        {
            OfferId = offer.Id,
            Inquiry = offer.Inquiry?.ToDto()!,
            CreationDate = offer.CreationDate,
            ExpireDate = offer.ExpireDate,
            UpdateDate = offer.UpdateDate,
            Status = offer.Status,
            Price = offer.Price,
            ReasonOfRejection = offer.ReasonOfRejection,
            CustomerInfo = offer.CustomerInfo?.ToDto(),
            OrderID = offer.OrderID
        };
    }
    public static OrderDTO ToDTO(this Order order)
    {
        return new()
        {
            Id = order.Id,
            OfferID = order.OfferID,
            Offer = order.Offer.ToDTO(),
            OrderStatus = order.OrderStatus,
            Comment = order.Comment,
            LastUpdate = order.LastUpdate,
            CourierName = order.CourierName
        };
    }
    public static OfferInfo ToDTO(this GetOfferResponse response)
    {
        return new()
        {
            OfferId = response.OfferId,
            Package = new Package()
            {
                Length = (int)(response.Dimensions.Length * 100),
                Height = (int)(response.Dimensions.Height * 100),
                Width = (int)(response.Dimensions.Width * 100),
                Weight = response.Weight
            },
            Source = response.Source.ToDto(),
            Destination = response.Destination.ToDto(),
            PickupDate = response.PickupDate,
            DeliveryDate = response.DeliveryDate,
            DeliveryInWeekend = response.DeliveryInWeekend,
            HighPriority = response.Priority == "High",
            PriceItems = response.PriceBreakDown,
            TotalPrice = response.TotalPrice,
            LastUpdateDate = response.DecisionDate,
            OfferStatus = response.OfferStatus!,
            BuyerName = response.BuyerName!,
            BuyerAddress = response.BuyerAddress.ToDto()
        };
    }
    public static OfferInfo ToInfo(this Offer offer)
    {
        return new()
        {
            OfferId = offer.Id.ToString(),
            Package = offer.Inquiry.Package,
            Source = offer.Inquiry.SourceAddress.ToDto(),
            Destination = offer.Inquiry.DestinationAddress.ToDto(),
            PickupDate = offer.Inquiry.PickupDate,
            DeliveryDate = offer.Inquiry.DeliveryDate,
            DeliveryInWeekend = offer.Inquiry.DeliveryAtWeekend,
            HighPriority = offer.Inquiry.HighPriority,
            PriceItems = offer.Price.ToDto(),
            TotalPrice = offer.Price.ToDto().Sum(x => x.Amount),
            LastUpdateDate = offer.UpdateDate,
            OfferStatus = offer.Status.ToString(),
            BuyerName = $"{offer.CustomerInfo!.FirstName} {offer.CustomerInfo.LastName}",
            BuyerAddress = offer.CustomerInfo.Address.ToDto()
        };
    }
}
