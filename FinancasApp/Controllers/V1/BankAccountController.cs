using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancasApp.Controllers.V1;

[ApiController]
[Route("api/v1/bank-accounts")]
[Authorize]
public class BankAccountController : ControllerBase
{
    private BankAccountService _bankAccountService;

    public BankAccountController(BankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBankAccountRequest request)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var created = await _bankAccountService.CreateAsync(userEmail, request);

        return Ok(created);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 1
    )
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var data = await _bankAccountService.GetAllPaginatedAsync(pageIndex, pageSize, userEmail);
        return Ok(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneAsync([FromRoute] Guid id)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var data = await _bankAccountService.GetOneAsync(userEmail, id);
        return Ok(data);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        await _bankAccountService.DeleteAsync(userEmail, id);
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateBankAccountRequest request, [FromRoute] Guid id)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var update = await _bankAccountService.UpdateAsync(userEmail, request, id);
        return Ok(update);
    }

}
