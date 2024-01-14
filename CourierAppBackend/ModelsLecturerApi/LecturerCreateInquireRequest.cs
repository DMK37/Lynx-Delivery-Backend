namespace CourierAppBackend.ModelsLecturerApi
{
    public class LecturerCreateInquireRequest
    {
        public Dimensions Dimensions { get; set; }
        public string Currency { get; set; } = "Pln";
        public float Weight { get; set; }
        public string WeightUnit { get; set; } = "Kilograms";
        public Address source { get; set; }
        public Address destination { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDay { get; set; }
        public bool deliveryInWeekend { get; set; }
        public string Priority { get; set; }
        public bool VipPackage { get; set; } = false;
        public bool isComapny { get; set; }

    }
}
