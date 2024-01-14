using CourierAppBackend.ModelsLecturerApi;

namespace CourierAppBackend.ModelsDTO
{
    public class OfferInfo
    {
        public int Id { get; set; }
        public int? OfferId { get; set; }
        public string Company { get; set; }
        public string InquiryId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }
    }
}
