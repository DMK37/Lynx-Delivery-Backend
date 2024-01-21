using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Auth;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.LynxDeliveryAPI;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[Route("api/public")]
[ApiController]
[ApiExplorerSettings(GroupName = "public")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class PublicController(IOffersRepository offersRepository, IOrdersRepository ordersRepository, IMessageSender messageSender)
    : ControllerBase
{

    [HttpPost("offers")]
    [ProducesResponseType(typeof(CreateOfferResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateOfferResponse>> CreateOffer([FromBody] CreateOfferRequest request)
    {
        var response = await offersRepository.CreateOffer(request);
        return CreatedAtRoute("GetOffer", new { id = response.OfferId }, response);
    }

    [HttpGet("offers/{id}", Name = "GetOffer")]
    public async Task<ActionResult<GetOfferResponse>> GetOffer(int id)
    {
        var offer = await offersRepository.GetOffer(id);
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

    [HttpPatch("offers/{id}/confirm")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Offer>> ConfirmOffer([FromRoute] int id, [FromBody] ConfirmOfferRequest request)
    {
        var offer = await offersRepository.ConfirmOffer(id, request);
        if (offer is null)
            return BadRequest();
        await messageSender.SendOfferSelectedMessage(offer);
        return Ok();
    }


    [HttpGet("orders/{id}", Name = "GetOrder")]
    public async Task<ActionResult<GetOfferResponse>> GetOrder(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var order = await ordersRepository.GetOrderById(id);
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
