
using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Abstractions.Services;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;

using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[Route("api/orders")]
[ApiController]
[ApiExplorerSettings(GroupName = "private")]
public class OrdersController(IOrdersRepository ordersRepository, IMessageSender messageSender, IFileService fileService)
    : ControllerBase
{
    // GET: api/orders
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetAll()
    {
        var orders = await ordersRepository.GetAll();
        return orders.Count > 0 ? Ok(orders) : NotFound();
    }

    // GET api/orders/{id}
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get([FromRoute] int id)
    {
        var order = await ordersRepository.GetOrderById(id);
        return order is null ? NotFound() : Ok(order);
    }

    // PATCH api/orders/{id}
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpPatch("{id}")]
    public async Task<ActionResult<Order>> UpdateOrder([FromRoute] int id, [FromBody] OrderUpdate orderUpdate)
    {
        var order = await ordersRepository.UpdateOrder(id, orderUpdate);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost("test")]
    public async Task<IActionResult> test()
    {
        return Ok();
    }


    // just to test upload functionality
    [HttpPost("upload")]
    public async Task<IActionResult> upload(IFormFile file)
    {
        String s = await fileService.SaveFile(file);
        if (s == "")
            return BadRequest();
        return Ok(s);
    }

}

