namespace CourierAppBackend.Models.Database
{
    public class TemporaryOffer : Base
    {
        public Inquiry Inquiry { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string InquiryID { get; set; } = string.Empty;
        public int OfferID { get; set; }
        public string Currency { get; set; } = null!;
        public decimal TotalPrice;
        public List<PriceItem> PriceItems { get; set; } = null!;
        public DateTime ExpiringAt { get; set; }
    }
}
