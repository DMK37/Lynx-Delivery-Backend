
namespace CourierAppBackend.Models;

public class Address : Base
{
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ApartmentNumber { get; set; }
}
