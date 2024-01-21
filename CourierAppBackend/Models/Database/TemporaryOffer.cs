namespace CourierAppBackend.Models.Database
{
    public class TemporaryOffer : Base
    {
        public Inquiry Inquiry { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string InquiryID { get; set; } = string.Empty;
        public string OfferRequestId { get; set; } = string.Empty;
        public string OfferID { get; set; } = string.Empty;
        public string Currency { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public List<PriceItem> PriceItems { get; set; } = null!;
        public DateTime ExpiringAt { get; set; }
    }
}
