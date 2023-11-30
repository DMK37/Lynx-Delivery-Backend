namespace CourierAppBackend.Models;

public class OrderHistory : Base
{
    public int OrderID { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime Date { get; set; }
    public string CourierName { get; set; } = null!;
}
