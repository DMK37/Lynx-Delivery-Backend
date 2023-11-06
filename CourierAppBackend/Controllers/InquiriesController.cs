using CourierAppBackend.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[ApiController]
[Route("api/inquiries")]
public class InquiriesController : ControllerBase
{
    private readonly IInquiriesRepository _inquiriesRepository;

    public InquiriesController(IInquiriesRepository repository)
    {
        _inquiriesRepository = repository;
    }

    // GET: api/inquiries/{id}
    [HttpGet("{id}")]
    public IActionResult GetInquiryById(int id)
    {
        var inquiry = _inquiriesRepository.GetInquiryById(id);
        if (inquiry is null)
            return NotFound();
        return Ok(inquiry);
    }

    // GET: api/inquiries
    [HttpGet]
    public IActionResult GetAll()
    {
        var inquiries = _inquiriesRepository.GetAll();
        if (inquiries is null || inquiries.Count == 0)
            return NotFound();
        return Ok(inquiries);
    }

}
