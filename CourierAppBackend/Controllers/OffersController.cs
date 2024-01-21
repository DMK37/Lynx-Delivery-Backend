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
public class OffersController(IOffersRepository offersRepository, IOrdersRepository ordersRepository, IMessageSender messageSender, IEnumerable<IApiCommunicator> apis, IFileService fileService)
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
    [ProducesResponseType(typeof(OfferDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OfferDTO>> GetOffer([FromRoute] int id)
    {
        var offer = await offersRepository.GetOfferById(id);
        return offer is not null ? Ok(offer) : NotFound();
    }

    // GET: api/offers
    [ProducesResponseType(typeof(List<OfferDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet]
    //[Authorize("read:all-offers")]
    public async Task<ActionResult<List<Offer>>> GetOffers()
    {
        var offers = await offersRepository.GetAll();
        return offers is not null ? Ok(offers) : NotFound();
    }

    // GET: api/offers/pending
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(List<OfferDTO>), StatusCodes.Status404NotFound)]
    [HttpGet("pending")]
    //[Authorize("read:all-offers")]
    public async Task<ActionResult<List<OfferDTO>>> GetPendingOffers()
    {
        var offers = await offersRepository.GetPending();
        return offers is not null ? Ok(offers) : NotFound();
    }

    // POST: api/offers/{id}/accept
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpPost("{id}/accept")]
    public async Task<IActionResult> AcceptOffer([FromRoute] int id, IFormFile agreement, IFormFile receipt)
    {
        String s = await fileService.SaveFile(agreement);
        if (s == "")
            return NotFound();
        s = await fileService.SaveFile(receipt);
        if (s == "")
            return NotFound();
        
        var accepted = await offersRepository.AcceptOffer(id);
        if (!accepted) return NotFound();
        var order = await ordersRepository.GetOrderByOfferId(id);
        if (order is not null)
            await messageSender.SendOrderCreatedMessage(order);
        return accepted ? Ok() : NotFound();
    }

    // POST: api/offers/{id}/reject
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpPost("{id}/reject")]
    public async Task<IActionResult> RejectOffer([FromRoute] int id, [FromBody] string reason)
    {
        var offers = await offersRepository.RejectOffer(id, reason);
        return offers ? Ok() : NotFound();
    }
}