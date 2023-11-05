using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController: ControllerBase
{
    private readonly IInquiriesRepository _inquiriesRepository;
    
    public ClientController(IInquiriesRepository repository)
    {
        _inquiriesRepository = repository;
    }

    [HttpGet("{id:int}/inquiries")]
    public ActionResult<List<Inquiry>> GetLastInquiries(int id)
    {
        var inquiries = _inquiriesRepository.GetLastInquiries(id);
        if (inquiries.Count == 0)
        {
            return NotFound();
        }
        return Ok(inquiries);
    }
}