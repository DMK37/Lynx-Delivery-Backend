using CourierAppBackend.Abstractions;
using CourierAppBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourierAppBackend.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController: ControllerBase
{
    private readonly IInquiriesRepository _inquiriesRepository;
    private readonly IUserRepository _usersRepo;

    
    public ClientController(IInquiriesRepository repository, IUserRepository usersRepo)
    {
        _inquiriesRepository = repository;
        _usersRepo = usersRepo;
    }

    [HttpPost] // change to https
    [Route("register")]
    public IActionResult Register([FromForm] User user)
    {
        // validation should be handled in frontend
         if(ModelState.IsValid) 
         {
            var newUser = new User 
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password, // hash & add salt parameter
                Address = user.Address,
                DefaultSourceAddress = user.DefaultSourceAddress,

                Role = user.Role  // bind to enum in frontend
            };

            _usersRepo.Add(newUser);
            return Ok("Registered.");
         }
         return BadRequest("Invalid data.");
    }

    [HttpGet("{id:int}/inquiries")]
    public ActionResult<List<Inquiry>> GetLastInquiries(int id)
    {
        var inquiries = _inquiriesRepository.GetLastInquiries(id);
        if (inquiries.Count == 0)
        {
            return NotFound();
        }
        return Ok(inquiries);
    }
}