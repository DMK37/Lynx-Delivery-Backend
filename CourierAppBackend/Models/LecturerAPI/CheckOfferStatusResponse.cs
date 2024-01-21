namespace CourierAppBackend.Models.LecturerAPI;

public class CheckOfferStatusResponse
{
    public string? OfferId {  get; set; }
    public bool IsReady {  get; set; }
    public DateTime TimeStamp { get; set; }
}
