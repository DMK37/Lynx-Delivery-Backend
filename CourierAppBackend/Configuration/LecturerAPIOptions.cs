namespace CourierAppBackend.Configuration;

public class LecturerAPIOptions
{
    public string client_id { get; set; } = null!;
    public string client_secret { get; set; } = null!;
    public string Scope { get; set; } = null!;
    public string tokenEndPoint { get; set; } = null!;
    public string apiEndPoint { get; set; } = null!;
}
