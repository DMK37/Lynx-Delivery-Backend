namespace CourierAppBackend.DtoModels
{
    public class AddressDTO
    {
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string ApartmentNumber { get; set; } = null!;
    }
}
