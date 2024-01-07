using CourierAppBackend.Abstractions;
using CourierAppBackend.Auth;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsPublicDTO;
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
    public PublicController(IOffersRepository offersRepository)
    {
        _offersRepository = offersRepository;
    }

    // POST: api/public/offers
    [HttpPost("offers")]
    public async Task<ActionResult<CreateOfferResponse>> CreateOffer([FromBody] CreateOfferRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();
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
        return CreatedAtRoute("PostOffer", new { ID = response.OfferId }, response);
    }

    // GET: api/public/offers/{id}
    [HttpGet("offers/{id}", Name = "GetOffer")]
    public async Task<ActionResult<GetOfferResponse>> GetOffer(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
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
}
