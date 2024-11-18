using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancasApp.Controllers.V1;
[Route("api/v1/expense-categories")]
[ApiController]
public class ExpenseCategoryController : ControllerBase
{
    private readonly ExpenseCategoryService _service;

    public ExpenseCategoryController(ExpenseCategoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateExpenseCategoryRequest request)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var created = await _service.CreateAsync(userEmail, request);

        return Ok(created);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 1
    )
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var data = await _service.GetAllAsync(userEmail, pageIndex, pageSize);
        return Ok(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneAsync(Guid id)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var data = await _service.GetOneAsync(userEmail, id);
        return Ok(data);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateExpenseCategoryRequest request)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var updated = await _service.UpdateAsync(userEmail, id, request);
        return Ok(updated);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        await _service.DeleteAsync(userEmail, id);
        return NoContent();

    }
}