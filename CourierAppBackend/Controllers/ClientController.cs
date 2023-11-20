using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CourierAppBackend.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController: ControllerBase
{
    private readonly IInquiriesRepository _inquiriesRepository;
    private readonly IUserInfoRepository _usersInfosRepo;

    
    public ClientController(IInquiriesRepository repository, IUserInfoRepository usersInfosRepo)
    {
        _inquiriesRepository = repository;
        _usersInfosRepo = usersInfosRepo;
    }


    [HttpPost("user-info")]
    [Authorize("edit:profile")]
    public ActionResult<UserInfo> CreateUserInfo([FromBody] UserInfo userInfo)
    {
        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        userInfo.UserId = userId!;

        var userInf = _usersInfosRepo.CreateUserInfo(userInfo);
            if(userInf is null)
                return BadRequest();
            return CreatedAtRoute("Get", new { ID = userInfo.UserId }, userInfo);
    }

    [HttpGet("{id}/inquiries")]
    public ActionResult<List<Inquiry>> GetLastInquiries(string id)
    {
        var inquiries = _inquiriesRepository.GetLastInquiries(id);
        if (inquiries.Count == 0)
        {
            return NotFound();
        }
        return Ok(inquiries);
    }
    
    [HttpGet("user-info")]
    [Authorize("get:profile")]
    public ActionResult<Offer> GetUserInfo()
    {

        var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized("Provide userId in token");
        }
        var offer = _usersInfosRepo.GetUserInfoById(userId);
        if (offer is null)
            return NotFound("User Not Found");
        return Ok(offer);
    }
}