using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "private")]
[Route("api/inquiries")]
public class InquiriesController(IInquiriesRepository repository) 
    : ControllerBase
{
    // GET: api/inquiries/{id}
    [ProducesResponseType(typeof(InquiryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetInquiry")]
    public async Task<ActionResult<InquiryDTO>> GetInquiryById([FromRoute] int id)
    {
        var inquiry = await repository.GetInquiryById(id);
        return inquiry is null ? NotFound() : Ok(inquiry);
    }

    // GET: api/inquiries
    [HttpGet]
    [ProducesResponseType(typeof(List<InquiryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [Authorize("read:all-inquiries")]
    public async Task<ActionResult<List<InquiryDTO>>> GetAll()
    {
        var inquiries = await repository.GetAll();
        return Ok(inquiries);
    }

    // POST: api/inquiries
    [HttpPost]
    [ProducesResponseType(typeof(InquiryDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InquiryDTO>> CreateInquiry([FromBody] InquiryCreate inquiryCreate)
    {
        var inquiry = await repository.CreateInquiry(inquiryCreate);
        return CreatedAtRoute("GetInquiry", new { inquiry.Id }, inquiry);
    }
}
