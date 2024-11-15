using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("users")]
    public async Task<IActionResult> Registrate([FromBody] CreateNewUserRequest request)
    {
        var user = await _identityService.CreateNewUser(request);
        if (user.Errors.Count > 0)
            return BadRequest(user);

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var token = await _identityService.Login(request);
        if (token.Errors.Count > 0)
        {
            return BadRequest(token);
        }

        return Ok(token);
    }
}
