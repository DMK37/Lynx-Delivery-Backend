using CourierAppBackend.Abstractions.Repositories;
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
    // POST: api/client/user-info
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("user-info")]
    [Authorize("edit:profile")]
    public async Task<ActionResult<UserDTO>> EditUser([FromBody] UserDTO userDTO)
    {
        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        userDTO.UserId = userId;
        var user = await usersRepository.EditUser(userDTO);
        return Ok(user);
    }

    // GET: api/client/user-info
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    // GET: api/client/inquiries
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("inquiries")]
    [Authorize("get:last-inquiries")]
    public async Task<ActionResult<List<InquiryDTO>>> GetLastInquiries()
    {
        string userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
        var inquiries = await repository.GetLastInquiries(userId);
        return Ok(inquiries);
    }
}