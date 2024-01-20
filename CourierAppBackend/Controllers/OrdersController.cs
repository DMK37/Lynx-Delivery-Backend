using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using CourierAppBackend.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourierAppBackend.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "private")]
    public class OrdersController : ControllerBase
    {
        private IOrdersRepository _ordersRepository;
        private IInquiriesRepository _inquiriesRepository;
        private IMessageSender _messageSender;
        private IApiCommunicator _contactLecturerApi;
        public OrdersController(IOrdersRepository ordersRepository, IMessageSender messageSender, IApiCommunicator api, IInquiriesRepository inquiriesRepository)
        {
            _ordersRepository = ordersRepository;
            _messageSender = messageSender;
            _contactLecturerApi = api;
            _inquiriesRepository = inquiriesRepository;
        }
        // for courier 
        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            var orders = await _ordersRepository.GetOrders();
            if (orders is null)
                return BadRequest();
            return Ok(orders);
        }

        // GET api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _ordersRepository.GetOrderById(id);
            if (order is null)
                return NotFound();
            return Ok(order);
        }
        // endpoint for office worker to accept given offer
        // POST api/orders
        [HttpPost(Name = "PostOrder")]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderC orderC)
        {
            var order = await _ordersRepository.CreateOrder(orderC);
            if (order is null)
                return BadRequest();
            await _messageSender.SendOrderCreatedMessage(order);
            return CreatedAtRoute("PostOrder", new { ID = order.Id }, order);
        }

        // PATCH api/orders/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] OrderU orderU)
        {
            var order = await _ordersRepository.UpdateOrder(id,orderU);
            if (order is null)
                return BadRequest();
            return Ok(order);
        }
        [HttpPost("test")]
        public async Task<ActionResult> Test()
        {
            var inq = await _inquiriesRepository.GetInquiryById(79);

            var sth = await _contactLecturerApi.GetOffer(inq);

            return Ok();
        }
    }
}
