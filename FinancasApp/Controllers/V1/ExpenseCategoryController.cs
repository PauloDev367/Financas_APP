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
    public async Task<IActionResult> CreateAsync(){}
    [HttpGet]
    public async Task<IActionResult> GetOneAsync(){}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllAsync(){}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(){}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(){}
}