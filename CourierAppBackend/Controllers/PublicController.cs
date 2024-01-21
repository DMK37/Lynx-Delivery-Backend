using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Auth;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.LynxDeliveryAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[Route("api/public")]
[ApiController]
[ApiExplorerSettings(GroupName = "public")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class PublicController(IOffersRepository offersRepository, IOrdersRepository ordersRepository, IMessageSender messageSender)
    : ControllerBase
{
    // POST: api/public/offers
    [ProducesResponseType(typeof(CreateOfferResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpPost("offers")]
    public async Task<ActionResult<CreateOfferResponse>> CreateOffer([FromBody] CreateOfferRequest request)
    {
        var response = await offersRepository.CreateOfferAPI(request);
        return response is not null ? CreatedAtRoute("GetOffer", new { id = response.OfferId }, response) : BadRequest();
    }

    // GET: api/public/offers/{id}
    [ProducesResponseType(typeof(CreateOfferResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("offers/{id}", Name = "GetOffer")]
    public async Task<ActionResult<GetOfferResponse>> GetOffer(int id)
    {
        var offer = await offersRepository.GetOfferAPI(id);
        return offer is null ? NotFound() : Ok(offer);
    }

    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpPatch("offers/{id}/confirm")]
    public async Task<IActionResult> ConfirmOffer([FromRoute] int id, [FromBody] ConfirmOfferRequest request)
    {
        var confirmed = await offersRepository.ConfirmOfferAPI(id, request);
        if (confirmed)
        {
            var offer = await offersRepository.GetOffer(id);
            if (offer is not null)
                await messageSender.SendOfferSelectedMessage(offer);
            return Ok();
        }
        return BadRequest();
    }

    [ProducesResponseType(typeof(GetOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("orders/{id}", Name = "GetOrder")]
    public async Task<ActionResult<GetOrderResponse>> GetOrder(int id)
    {
        var order = await ordersRepository.GetOrderAPI(id);
        return order is null ? NotFound() : Ok(order);
    }
}