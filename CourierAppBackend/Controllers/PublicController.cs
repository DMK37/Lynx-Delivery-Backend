using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Auth;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.LynxDeliveryAPI;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

// the controller to use our api for other groups
[Route("api/public")]
[ApiController]
[ApiExplorerSettings(GroupName = "public")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class PublicController : ControllerBase
{
    private readonly IOffersRepository _offersRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMessageSender _messageSender;
    public PublicController(IOffersRepository offersRepository, IOrdersRepository ordersRepository, IMessageSender messageSender)
    {
        _offersRepository = offersRepository;
        _ordersRepository = ordersRepository;
        _messageSender = messageSender;
    }

    // POST: api/public/offers
    [HttpPost("offers")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateOfferResponse>> CreateOffer([FromBody] CreateOfferRequest request)
    {
        var offer = await _offersRepository.CreateOfferFromRequest(request);
        if (offer is null)
            return BadRequest();
        var response = new CreateOfferResponse
        {
            OfferId = offer.Id,
            CreationDate = offer.CreationDate,
            ExpireDate = offer.ExpireDate,
            Price = offer.Price
        };
        return CreatedAtRoute("GetOffer", new { ID = response.OfferId }, response);
    }

    [HttpPatch("offers/{id}/confirm")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Offer>> ConfirmOffer([FromRoute] int id, [FromBody] ConfirmOfferRequest request)
    {
        var offer = await _offersRepository.ConfirmOffer(id, request);
        if (offer is null)
            return BadRequest();
        await _messageSender.SendOfferSelectedMessage(offer);
        return Ok();
    }

    // GET: api/public/offers/{id}
    [HttpGet("offers/{id}", Name = "GetOffer")]
    public async Task<ActionResult<GetOfferResponse>> GetOffer(int id)
    {
        var offer = await _offersRepository.GetOfferById(id);
        if (offer is null)
            return NotFound();
        var response = new GetOfferResponse
        {
            OfferId = offer.Id,
            PickupDate = offer.Inquiry.PickupDate,
            DeliveryDate = offer.Inquiry.DeliveryDate,
            Package = offer.Inquiry.Package,
            SourceAddress = offer.Inquiry.SourceAddress,
            DestinationAddress = offer.Inquiry.DestinationAddress,
            IsCompany = offer.Inquiry.IsCompany,
            HighPriority = offer.Inquiry.HighPriority,
            DeliveryAtWeekend = offer.Inquiry.DeliveryAtWeekend,
            CreationDate = offer.CreationDate,
            ExpireDate = offer.ExpireDate,
            UpdateDate = offer.UpdateDate,
            Status = offer.Status,
            ReasonOfRejection = offer.ReasonOfRejection,
            Price = offer.Price
        };
        return Ok(response);
    }

    [HttpGet("orders/{id}", Name = "GetOrder")]
    public async Task<ActionResult<GetOfferResponse>> GetOrder(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var order = await _ordersRepository.GetOrderById(id);
        if (order is null)
            return NotFound();
        var response = new GetOrderResponse
        {
            OfferID = order.OfferID,
            OrderStatus = order.OrderStatus,
            LastUpdate = order.LastUpdate,
            Comment = order.Comment,
            CourierName = order.CourierName
        };
        return Ok(response);
    }

}
