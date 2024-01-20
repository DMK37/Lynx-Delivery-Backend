using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Models.LynxDeliveryAPI;

public class ConfirmOfferRequest
{
    public CustomerInfoDTO CustomerInfo { get; set; } = null!;
}
