using CourierAppBackend.Abstractions;
using CourierAppBackend.Data;
using CourierAppBackend.DtoModels;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers
{
    [Route("api/offers")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOffersRepository _offersRepository;
        public OffersController(IOffersRepository offersRepository)
        {
            _offersRepository = offersRepository;
        }
        
        // for other groups
        // POST: api/offers
        [HttpPost]
        public async Task<ActionResult<Offer>> CreateOffer([FromBody] CreateOffer createOffer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var offer = await _offersRepository.CreateNewOffer(createOffer);
            if(offer is null)
                return BadRequest();
            return CreatedAtRoute("Get", new { ID = offer.Id }, offer);
        }

        // endpoint to get offers from all couriers
        // POST: api/offers/getAll
        [HttpPost("getAll")]
        public async Task<ActionResult<Offer>> CreateOffers([FromBody] CreateAllOffers createOffers)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // TODO: create a way to manage different API's
            var offer = await _offersRepository.CreateOffferFromOurInquiry(createOffers);
            if (offer is null)
                return BadRequest();
            return CreatedAtRoute("Get", new { ID = offer.Id }, offer);
        }

        // GET: api/offers/{id}
        [HttpGet]
        public async Task<ActionResult<Offer>> GetOffer(int id)
        {
            var offer = await _offersRepository.GetOfferById(id);
            if (offer is null)
                return NotFound();
            return Ok(offer);
        }
    }
}
