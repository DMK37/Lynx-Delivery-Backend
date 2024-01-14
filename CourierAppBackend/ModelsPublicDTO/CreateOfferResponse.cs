using CourierAppBackend.Models;

namespace CourierAppBackend.ModelsPublicDTO
{
    public class CreateOfferResponse
    {
        public int OfferId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Price Price { get; set; } = null!;
    }
}
