namespace CourierAppBackend.ModelsLecturerApi
{
    public class LecturerCreateInquireResponse
    {
        public string InquiryId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; }

    }
}
