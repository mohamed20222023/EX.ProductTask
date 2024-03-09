using Application.Dtos.Auth.Users;
using Application.Dtos.Users;
using Application.IBusiness.Management;
using Core.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
namespace Users.API.Controllers.Management;
[Route("api/[controller]")]
[ApiController]
public class UsersAppController : ControllerBase
{
    private readonly IUserBusiness _iUserRepository;

    public UsersAppController(
     IUserBusiness IUserRepository
      )
    {
        _iUserRepository = IUserRepository;
    }
    [HttpPost]

    public async Task<IActionResult> Get()
    {
        var result =  _iUserRepository.Get();
        return Ok(result);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetUser(int id)
    {
        var result = await _iUserRepository.GetUser(id);
        if (result.Success)
            return Ok(result);
        else
            return BadRequest(result.Message);
    }





    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, [FromForm] UserEditDto userEdit)
    {
        await _iUserRepository.Edit(id, userEdit);
        return NoContent();

    }

    
}

