using Microsoft.AspNetCore.Mvc;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Services;

namespace FinancasApp.Controllers.V1;


[ApiController]
[Route("api/v1/")]
public class AuthController : ControllerBase
{
    private readonly IdentityService _identityService;

    public AuthController(IdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateNewUserRequest request)
    {
        var result = await _identityService.Register(request);
        if (result.Succeeded)
        {
            return Ok(new { message = "User registered successfully" });
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var login = await _identityService.Login(request);
        if (request != null)
        {
            return Ok(new { login });
        }
        return Unauthorized();
    }


}
