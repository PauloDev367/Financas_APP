using System;
using FinancasApp.Configurations;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancasApp.Controllers.V1;

[ApiController]
[Route("api/v1/income-categories")]
public class IncomeCategoryController : ControllerBase
{
    private readonly IncomeCategoryService _service;
    private readonly RequestUser _requestUser;
    public IncomeCategoryController(IncomeCategoryService service, RequestUser requestUser)
    {
        _service = service;
        _requestUser = requestUser;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateIncomeCategoryRequest request)
    {
        var created = await _service.CreateAsync(_requestUser.User, request);

        return Ok(created);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 1
    )
    {
        var data = await _service.GetAllAsync(_requestUser.User, pageIndex, pageSize);
        return Ok(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneAsync(Guid id)
    {
        var data = await _service.GetOneAsync(_requestUser.User, id);
        return Ok(data);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateIncomeCategoryRequest request)
    {
        var updated = await _service.UpdateAsync(_requestUser.User, id, request);
        return Ok(updated);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(_requestUser.User, id);
        return NoContent();

    }
}
