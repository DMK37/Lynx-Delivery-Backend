namespace CourierAppBackend.Models.LecturerAPI;

public class CreateInquireRequest
{
    public Dimensions Dimensions { get; set; } = null!;
    public string Currency { get; set; } = "Pln";
    public float Weight { get; set; }
    public string WeightUnit { get; set; } = "Kilograms";
    public LecturerAddress Source { get; set; } = null!;
    public LecturerAddress Destination { get; set; } = null!;
    public DateTime PickupDate { get; set; }
    public DateTime DeliveryDay { get; set; }
    public bool deliveryInWeekend { get; set; }
    public string Priority { get; set; } = null!;
    public bool VipPackage { get; set; } = false;
    public bool isComapny { get; set; }
}