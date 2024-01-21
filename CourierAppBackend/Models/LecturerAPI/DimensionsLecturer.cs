namespace CourierAppBackend.Models.LecturerAPI;

public class DimensionsLecturer
{
    public float Width { get; set; }
    public float Height { get; set; }
    public float Length { get; set; }
    public string DimensionUnit { get; set; } = "Meters";
}