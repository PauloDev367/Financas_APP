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
    public async Task<IActionResult> CreateAsync() { 
        // var response = await _service.CreateAsync(_userEmail, request);

        return Ok(_requestUser);

    }
    // [HttpPut]
    // public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateEntryRequest request) { 
    //     var response = await _service.UpdateAsync(_userEmail, id, request);

    // }
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteAsync(Guid id) { 
    //     var response = await _service.DeleteAsync(id);

    // }
    // [HttpPatch("{id}/received")]
    // public async Task<IActionResult> ChangeReceivedStatusAsync([FromBody] UpdateEntryReceivedStatusRequest request, Guid id) { 
    //     var response = await _service.ChangeReceivedStatusAsync(_userEmail, id, request);

    // }
    // [HttpPatch("{id}/entry-type")]
    // public async Task<IActionResult> UpdateEntryTypeAsync([FromBody] UpdateEntryTypeRequest request, Guid id) { 
    //     var response = await _service.UpdateEntryTypeAsync(_userEmail, id, request);

    // }
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetOneAsync(Guid id) { 
    //     var response = await _service.GetOneAsync(_userEmail, id);

    // }
    // [HttpGet]
    // public async Task<IActionResult> GetAllAsync(
    //     [FromQuery] int pageIndex = 1,
    //     [FromQuery] int pageSize = 1
    // )
    // { 
    //     var response = await _service.GetAllAsync(_userEmail, pageIndex, pageSize);


    // }

}
