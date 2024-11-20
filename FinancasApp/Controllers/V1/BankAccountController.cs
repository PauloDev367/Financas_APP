using System;
using FinancasApp.Configurations;
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
    private readonly RequestUser _requestUser;
    public BankAccountController(BankAccountService bankAccountService, RequestUser requestUser)
    {
        _bankAccountService = bankAccountService;
        _requestUser = requestUser;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBankAccountRequest request)
    {
        var created = await _bankAccountService.CreateAsync(_requestUser.User, request);

        return Ok(created);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 1
    )
    {
        var data = await _bankAccountService.GetAllPaginatedAsync(pageIndex, pageSize, _requestUser.User);
        return Ok(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneAsync([FromRoute] Guid id)
    {
        var data = await _bankAccountService.GetOneAsync(_requestUser.User, id);
        return Ok(data);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _bankAccountService.DeleteAsync(_requestUser.User, id);
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateBankAccountRequest request, [FromRoute] Guid id)
    {
        var update = await _bankAccountService.UpdateAsync(_requestUser.User, request, id);
        return Ok(update);
    }

}
