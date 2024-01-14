using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace CourierAppBackend.ModelsPublicDTO;

public class CreateOfferRequest
{
    [Required]
    public DateTime PickupDate { get; set; }
    [Required]
    public DateTime DeliveryDate { get; set; }
    [Required]
    public Package Package { get; set; } = null!;
    [Required]
    public AddressDTO SourceAddress { get; set; } = null!;
    [Required]
    public AddressDTO DestinationAddress { get; set; } = null!;
    [Required]
    public bool IsCompany { get; set; }
    [Required]
    public bool HighPriority { get; set; }
    [Required]
    public bool DeliveryAtWeekend { get; set; }
}
