using CourierAppBackend.Abstractions;
using CourierAppBackend.Data;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using CourierAppBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers
{
    [Route("api/offers")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "private")]
    public class OffersController : ControllerBase
    {
        private readonly IOffersRepository _offersRepository;
        private readonly IMessageSender _messageSender;
        private readonly IEnumerable<IExternalApi> _externalApis;

        public OffersController(IOffersRepository offersRepository, IMessageSender messageSender, IEnumerable<IExternalApi> externalApis)
        {
            _offersRepository = offersRepository;
            _messageSender = messageSender;
            _externalApis = externalApis;
        }

        // for other groups
        // POST: api/offers
        [HttpPost(Name = "PostOffer")]
        public async Task<ActionResult<Offer>> CreateOffer([FromBody] OfferC createOffer)
        {
            var offer = await _offersRepository.CreateNewOffer(createOffer);
            if (offer is null)
                return BadRequest();
            return CreatedAtRoute("PostOffer", new { ID = offer.Id }, offer);
        }

        // endpoint to get offers from all couriers
        // POST: api/offers/getAll
        [HttpPost("getAll", Name = "PostOffers")]
        public async Task<ActionResult<List<OfferInfo>>> CreateOffers([FromBody] OfferAll createOffers)
        {

            // TODO: create a way to manage different API's
            var offers = await _offersRepository.GetOfferInfos(createOffers, _externalApis);
            if (offers is null)
                return BadRequest();
            return Ok(offers);
        }

        // endpoint to accept offer providing customer info
        [HttpPost("{id}/select")]
        public async Task<ActionResult<Offer>> SelectOffer([FromBody] OfferSelect createOffers)
        {
            var offer = await _offersRepository.SelectOffer(createOffers);
            if (offer is null)
                return BadRequest();
            await _messageSender.SendOfferSelectedMessage(offer);
            return CreatedAtRoute("PostOffers", new { ID = offer.Id }, offer);
        }

        // GET: api/offers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOffer(int id)
        {
            var offer = await _offersRepository.GetOfferById(id);
            if (offer is null)
                return NotFound();
            return Ok(offer);
        }

        [HttpGet]
        [Authorize("read:all-offers")]
        public async Task<ActionResult<List<Offer>>> GetOffers()
        {
            var offers = await _offersRepository.GetOffers();
            if (offers.Count == 0)
                return NotFound();
            return Ok(offers);
        }

        [HttpGet("pending")]
        [Authorize("read:all-offers")]
        public async Task<ActionResult<List<Offer>>> GetPendingOffers()
        {
            var offers = await _offersRepository.GetPendingOffers();
            if (offers.Count == 0)
                return NotFound();
            return Ok(offers);
        }
    }
}
