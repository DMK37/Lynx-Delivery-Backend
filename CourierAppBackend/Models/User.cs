
namespace CourierAppBackend.Models;

public enum Role
{
    User,
    OfficeWorker,
    Courier
}
public class User : Base
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Address Address { get; set; }
    public Address DefaultSourceAddress { get; set; }
    public Role Role { get; set; }
    public ICollection<Inquiry> Inquiries { get; set; }
}
