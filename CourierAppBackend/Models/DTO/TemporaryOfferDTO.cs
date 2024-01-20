using CourierAppBackend.Models.LecturerAPI;

namespace CourierAppBackend.Models.DTO
{
    public class TemporaryOfferDTO
    {
        public int Id { get; set; }
        public string Company { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceBreakDownItem> PriceBreakDown { get; set; } = null!;
    }
}
