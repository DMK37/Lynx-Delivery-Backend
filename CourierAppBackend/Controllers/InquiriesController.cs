using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourierAppBackend.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "private")]
[Route("api/inquiries")]
public class InquiriesController(IInquiriesRepository repository, IOffersRepository offersRepository,
    IEnumerable<IApiCommunicator> apis) 
    : ControllerBase
{
    // POST: api/inquiries/{id}/add
    [ProducesResponseType(typeof(InquiryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpPost("{id}/add")]
    public async Task<ActionResult<InquiryDTO>> AddInquiry([FromRoute] int id)
    {
        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        var inquiry = await repository.UpdateInquiry(userId, id);
        return inquiry is null? BadRequest() : Ok(inquiry);
    }

    // POST: api/inquiries
    [ProducesResponseType(typeof(InquiryDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<InquiryDTO>> CreateInquiry([FromBody] InquiryCreate inquiryCreate)
    {
        var inquiry = await repository.CreateInquiry(inquiryCreate);
        return CreatedAtRoute("GetInquiry", new { inquiry.Id }, inquiry);
    }

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
    [ProducesResponseType(typeof(List<InquiryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    //[Authorize("read:all-inquiries")]
    [HttpGet]
    public async Task<ActionResult<List<InquiryDTO>>> GetAll()
    {
        var inquiries = await repository.GetAll();
        return Ok(inquiries);
    }

    // POST: api/inquiries/{id}/offers
    [ProducesResponseType(typeof(List<TemporaryOfferDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [HttpPost("{id}/offers")]
    public async Task<ActionResult<List<TemporaryOfferDTO>>> CreateOffers([FromRoute] int id)
    {
        var offers = await offersRepository.GetOffers(id, apis.ToList());
        return offers is null ? BadRequest() : Ok(offers);
    }

    // GET: api/inquiries/{id}/offer
    [ProducesResponseType(typeof(OfferInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [HttpGet("{id}/offer")]
    public async Task<ActionResult<List<OfferInfo>>> GetOfferInfo([FromRoute] int id)
    {
        var offers = await offersRepository.GetOfferInfo(id, apis.ToList());
        return offers is null ? BadRequest() : Ok(offers);
    }
}
