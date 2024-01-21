namespace CourierAppBackend.Configuration
{
    public class SendGridOptions
    {
        public string FromEmail { get; set; } = null!;
        public string FromName { get; set; } = null!;
        public string OfferSelectedTemplate { get; set; } = null!;
        public string OfferAcceptedTemplate { get; set; } = null!;
    }
}
