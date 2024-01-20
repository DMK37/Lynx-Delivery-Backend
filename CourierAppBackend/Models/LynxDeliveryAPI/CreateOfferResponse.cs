using CourierAppBackend.Models.Database;

namespace CourierAppBackend.Models.LynxDeliveryAPI
{
    public class CreateOfferResponse
    {
        public int OfferId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Price Price { get; set; } = null!;
    }
}
