using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[Route("api/offers")]
[ApiController]
[ApiExplorerSettings(GroupName = "private")]
public class OffersController(IOffersRepository offersRepository, IMessageSender messageSender, IEnumerable<IApiCommunicator> apis)
    : ControllerBase
{
    // POST: api/offers/{id}/select
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [HttpPost("{id}/select")]
    public async Task<IActionResult> SelectOffer([FromRoute] int id, [FromBody] CustomerInfoDTO customerInfoDTO)
    {
        var offer = await offersRepository.SelectOffer(id, customerInfoDTO, apis.ToList());
        return offer ? Ok() : BadRequest();
    }

    // GET: api/offers/{id}
    [ProducesResponseType(typeof(Offer), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Offer>> GetOffer([FromRoute] int id)
    {
        var offer = await offersRepository.GetOfferById(id);
        if (offer is null)
            return NotFound();
        return Ok(offer);
    }

    // GET: api/offers
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpGet]
    [Authorize("read:all-offers")]
    public async Task<ActionResult<List<Offer>>> GetOffers()
    {
        var offers = await offersRepository.GetOffers();
        if (offers.Count == 0)
            return NotFound();
        return Ok(offers);
    }

    // GET: api/offers/pending
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpGet("pending")]
    [Authorize("read:all-offers")]
    public async Task<ActionResult<List<Offer>>> GetPendingOffers()
    {
        var offers = await offersRepository.GetPendingOffers();
        if (offers.Count == 0)
            return NotFound();
        return Ok(offers);
    }

    // POST: api/offers/{id}/accept
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpGet("{id}/accept")]
    public async Task<IActionResult> AcceptOffer([FromRoute] int id)
    {
        var offers = await offersRepository.GetPendingOffers();
        if (offers.Count == 0)
            return NotFound();
        return Ok(offers);
    }

    // POST: api/offers/{id}/reject
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpGet("{id}/reject")]
    public async Task<IActionResult> RejectOffer([FromRoute] int id, [FromBody] string reason)
    {
        var offers = await offersRepository.GetPendingOffers();
        if (offers.Count == 0)
            return NotFound();
        return Ok(offers);
    }

}
