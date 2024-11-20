using FinancasApp.Configurations;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancasApp.Controllers.V1;

[ApiController]
[Route("api/v1/entries")]
[Authorize]
public class EntryController : ControllerBase
{
    private readonly EntryService _service;
    private readonly RequestUser _requestUser;

    public EntryController(EntryService service, RequestUser requestUser)
    {
        _service = service;
        _requestUser = requestUser;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEntryRequest request)
    {
        var response = await _service.CreateAsync(_requestUser.User, request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneAsync(Guid id)
    {
        var response = await _service.GetOneAsync(_requestUser.User, id);
        return Ok(response);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 1,
        [FromQuery] int? year = null,
        [FromQuery] int? month = null
    )
    {
        var response = await _service.GetAllAsync(_requestUser.User, pageIndex, pageSize, year, month);
        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(_requestUser.User, id);
        return NoContent();
    }
    [HttpPatch("{id}/received")]
    public async Task<IActionResult> ChangeReceivedStatusAsync([FromBody] UpdateEntryReceivedStatusRequest request, Guid id)
    {
        var response = await _service.ChangeReceivedStatusAsync(_requestUser.User, id, request);
        return Ok(response);
    }
    // [HttpPatch("{id}/entry-type")]
    // public async Task<IActionResult> UpdateEntryTypeAsync([FromBody] UpdateEntryTypeRequest request, Guid id)
    // {
    //     var response = await _service.UpdateEntryTypeAsync(_requestUser.User, id, request);

    // }
    // [HttpPut]
    // public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateEntryRequest request)
    // {
    //     var response = await _service.UpdateAsync(_requestUser.User, id, request);

    // }


}
