using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourierAppBackend.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "private")]
[Route("api/client")]
public class ClientController(IInquiriesRepository inquiriesRepository, IUserRepository usersRepository, IOrdersRepository ordersRepository)
    : ControllerBase
{
    // POST: api/client/user-info
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [HttpPost("user-info")]
    //[Authorize("edit:profile")]
    public async Task<ActionResult<UserDTO>> EditUser([FromBody] UserDTO userDTO)
    {
        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        userDTO.UserId = userId;
        var user = await usersRepository.EditUser(userDTO);
        return Ok(user);
    }

    // GET: api/client/user-info
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("user-info")]
    //[Authorize("get:profile")]
    public async Task<ActionResult<UserDTO>> GetUserInfo()
    {
        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        var user = await usersRepository.GetUserById(userId);
        return user is null ? NotFound("User Not Found") : Ok(user);
    }

    // GET: api/client/inquiries
    [ProducesResponseType(typeof(List<InquiryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("inquiries")]
    //[Authorize("get:last-inquiries")]
    public async Task<ActionResult<List<InquiryDTO>>> GetLastInquiries()
    {
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        var inquiries = await inquiriesRepository.GetLastInquiries(userId);
        return inquiries.Count > 0 ? Ok(inquiries) : NotFound("No inquiries found");
    }

    // GET: api/client/orders
    [ProducesResponseType(typeof(List<InquiryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("orders")]
    public async Task<ActionResult<List<OrderDTO>>> GetOrders()
    {
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        var inquiries = await ordersRepository.GetUserOrders(userId);
        return inquiries.Count > 0 ? Ok(inquiries) : NotFound("No orders found");
    }
}