namespace CourierAppBackend.Models.DTO
{
    public class TemporaryOfferDTO
    {
        public int Id { get; set; }
        public string Company { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime ExpiringAt { get; set; }
        public List<PriceItemDTO> PriceBreakDown { get; set; } = null!;
    }
}
