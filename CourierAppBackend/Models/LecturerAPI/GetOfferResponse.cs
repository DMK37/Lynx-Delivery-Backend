using CourierAppBackend.Models.DTO;

namespace CourierAppBackend.Models.LecturerAPI
{
    public class GetOfferResponse
    {
        public string OfferId { get; set; } = null!;
        public Dimensions Dimensions { get; set; } = null!;
        public LecturerAddress Source { get; set;} = null!;
        public LecturerAddress Destination { get; set; } = null!;
        public float Weight { get; set; }
        public string? WeightUnit { get; set; }
        public DateTime PickupDate {  get; set; }
        public DateTime DeliveryDate {  get; set; }
        public DateTime ValidTo {  get; set; }
        public bool DeliveryInWeekend {  get; set; }
        public string? Priority {  get; set; }
        public bool VipPackage {  get; set; }
        public List<PriceItemDTO> PriceBreakDown { get; set; } = null!;
        public decimal TotalPrice {  get; set; }
        public string? Currency { get; set; }
        public DateTime InquireDate {  get; set; }
        public DateTime OfferRequestDate{ get; set;}
        public DateTime DecisionDate {  get; set; }
        public string? OfferStatus {  get; set; }
        public string? BuyerName { get; set; }
        public LecturerAddress BuyerAddress { get; set; } = null!;

    }
}
