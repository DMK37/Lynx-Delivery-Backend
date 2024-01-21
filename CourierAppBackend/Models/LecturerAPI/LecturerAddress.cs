namespace CourierAppBackend.Models.LecturerAPI;

public class LecturerAddress
{
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string ApartmentNumber { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = "Polska";
}