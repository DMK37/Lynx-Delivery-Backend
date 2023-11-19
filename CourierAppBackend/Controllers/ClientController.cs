using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
        var tokenString = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(tokenString) as JwtSecurityToken;
        var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
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
    
    [HttpGet("user-info/{userId}")]
    public ActionResult<Offer> GetUserInfo( string userId)
    {
        var offer = _usersInfosRepo.GetUserInfoById(userId);
        if (offer is null)
            return NotFound("User Not Found");
        return Ok(offer);
    }
}