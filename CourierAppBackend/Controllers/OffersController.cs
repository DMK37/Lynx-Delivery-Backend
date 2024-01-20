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

    // endpoint to accept offer providing customer info
    [HttpPost("{id}/select")]
    public async Task<ActionResult<Offer>> SelectOffer([FromBody] OfferSelect createOffers)
    {
        var offer = await offersRepository.SelectOffer(createOffers);
        if (offer is null)
            return BadRequest();
        await messageSender.SendOfferSelectedMessage(offer);
        return CreatedAtRoute("PostOffers", new { ID = offer.Id }, offer);
    }

    // GET: api/offers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Offer>> GetOffer(int id)
    {
        var offer = await offersRepository.GetOfferById(id);
        if (offer is null)
            return NotFound();
        return Ok(offer);
    }

    [HttpGet]
    [Authorize("read:all-offers")]
    public async Task<ActionResult<List<Offer>>> GetOffers()
    {
        var offers = await offersRepository.GetOffers();
        if (offers.Count == 0)
            return NotFound();
        return Ok(offers);
    }

    [HttpGet("pending")]
    [Authorize("read:all-offers")]
    public async Task<ActionResult<List<Offer>>> GetPendingOffers()
    {
        var offers = await offersRepository.GetPendingOffers();
        if (offers.Count == 0)
            return NotFound();
        return Ok(offers);
    }
}
