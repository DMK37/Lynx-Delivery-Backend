using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Models.LecturerAPI;
using Elfie.Serialization;

namespace CourierAppBackend.Services;

public static class TemporaryExtender
{
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
            source = new()
            {
                HouseNumber = inquiry.SourceAddress.HouseNumber,
                ApartmentNumber = inquiry.SourceAddress.ApartmentNumber,
                City = inquiry.SourceAddress.City,
                ZipCode = inquiry.SourceAddress.PostalCode
            },
            destination = new()
            {
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
}
