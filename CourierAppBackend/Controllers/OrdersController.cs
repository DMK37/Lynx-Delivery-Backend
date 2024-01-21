using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;
using CourierAppBackend.ModelsDTO;
using CourierAppBackend.Services;
using Microsoft.AspNetCore.Authorization;
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
        private IExternalApi _contactLecturerApi;
        private IFileService _fileService;

        public OrdersController(IOrdersRepository ordersRepository, IMessageSender messageSender, IExternalApi api,
            IInquiriesRepository inquiriesRepository, IFileService fileService)
        {
            _ordersRepository = ordersRepository;
            _messageSender = messageSender;
            _contactLecturerApi = api;
            _inquiriesRepository = inquiriesRepository;
            _fileService = fileService;
        }

        // for courier 
        // GET: api/orders
        [HttpGet]
        [Authorize("edit:order")]
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
        [Authorize("read:all-pending-offers")]
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
        [Authorize("edit:order")]
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

        
        // just to test upload functionality
        [HttpPost("upload")]
        public async Task<IActionResult> upload(IFormFile file)
        {
            String s = await _fileService.SaveFile(file);
            if (s == "")
                return BadRequest();
            return Ok(s);
        } 
    }
}
