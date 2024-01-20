using CourierAppBackend.Abstractions.Repositories;
using CourierAppBackend.Models.Database;
using CourierAppBackend.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourierAppBackend.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "private")]
[Route("api/client")]
public class ClientController(IInquiriesRepository repository, IUserRepository usersRepository)
    : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("user-info")]
    [Authorize("edit:profile")]
    public async Task<ActionResult<UserDTO>> EditUser([FromBody] UserDTO request)
    {
        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        request.UserId = userId;
        var response = await usersRepository.EditUser(request);
        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("user-info")]
    [Authorize("get:profile")]
    public async Task<ActionResult<UserDTO>> GetUserInfo()
    {
        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        var response = await usersRepository.GetUserById("userId");
        return response is null ? NotFound("User Not Found") : Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("inquiries")]
    [Authorize("get:last-inquiries")]
    public async Task<ActionResult<List<Inquiry>>> GetLastInquiries()
    {
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        var inquiries = await repository.GetLastInquiries(userId);
        if (inquiries.Count == 0)
        {
            return NotFound();
        }
        return Ok(inquiries);
    }
}