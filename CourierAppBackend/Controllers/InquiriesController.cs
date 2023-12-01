using CourierAppBackend.Abstractions;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using Microsoft.AspNetCore.Authorization;
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
    [HttpGet("{id}", Name ="Get")]
    public async Task<IActionResult> GetInquiryById(int id)
    {
        var inquiry = await _inquiriesRepository.GetInquiryById(id);
        if (inquiry is null)
            return NotFound();
        return Ok(inquiry);
    }

    // GET: api/inquiries
    [HttpGet]
    [Authorize("read:inquiries")]
    public async Task<IActionResult> GetAll()
    {
        var inquiries = await _inquiriesRepository.GetAll();
        if (inquiries is null || inquiries.Count == 0)
            return NotFound();
        return Ok(inquiries);
    }

    // POST: api/inquiries
    [HttpPost]
    [ProducesResponseType(typeof(Inquiry),StatusCodes.Status201Created)]
    public async Task<ActionResult<Inquiry>> CreateInquiry([FromBody]InquiryC inquiry)
    {
        var createdInquiry = await _inquiriesRepository.CreateInquiry(inquiry);
        if (createdInquiry is null)
            return BadRequest();
        return CreatedAtRoute("Get", new { ID = createdInquiry.Id }, inquiry);
    }

}
