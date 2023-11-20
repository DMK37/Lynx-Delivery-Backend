using CourierAppBackend.Models;

namespace CourierAppBackend.DtoModels
{
    public class CreateOffer
    {
        public int InquiryID {  get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Package Package { get; set; } = null!;
        public AddressDTO SourceAddress { get; set; } = null!;
        public AddressDTO DestinationAddress { get; set; } = null!;
        public bool HighPriority { get; set; }
        public bool DeliveryAtWeekend { get; set; }
    }
}
