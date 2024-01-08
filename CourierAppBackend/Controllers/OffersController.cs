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
        private readonly EmailSender _emailSender;

        public OffersController(IOffersRepository offersRepository, EmailSender emailSender)
        {
            _offersRepository = offersRepository;
            _emailSender = emailSender;
        }
        
        // for other groups
        // POST: api/offers
        [HttpPost(Name="PostOffer")]
        public async Task<ActionResult<Offer>> CreateOffer([FromBody] OfferC createOffer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var offer = await _offersRepository.CreateNewOffer(createOffer);
            if(offer is null)
                return BadRequest();
            return CreatedAtRoute("PostOffer", new { ID = offer.Id }, offer);
        }

        // endpoint to get offers from all couriers
        // POST: api/offers/getAll
        [HttpPost("getAll", Name = "PostOffers")]
        public async Task<ActionResult<Offer>> CreateOffers([FromBody] OfferAll createOffers)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            // TODO: create a way to manage different API's
            var offer = await _offersRepository.CreateOffferFromOurInquiry(createOffers);
            if (offer is null)
                return BadRequest();
            return CreatedAtRoute("PostOffers", new { ID = offer.Id }, offer);
        }

        // endpoint to accept offer providing customer info
        [HttpPost("{id}/select")]
        public async Task<ActionResult<Offer>> SelectOffer([FromBody] OfferSelect createOffers)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var offer = await _offersRepository.SelectOffer(createOffers);
            if (offer is null)
                return BadRequest();
            await _emailSender.SendOfferSelectedMessage(offer);
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
