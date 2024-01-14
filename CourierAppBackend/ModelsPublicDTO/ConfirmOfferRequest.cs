using CourierAppBackend.ModelsDTO;

namespace CourierAppBackend.ModelsPublicDTO;

public class ConfirmOfferRequest
{
    public CustomerInfoDTO CustomerInfo { get; set; } = null!;
}
