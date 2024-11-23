using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;

namespace FinancasApp.Services;

public class ExpenseCategoryService
{
    private readonly IExpenseCategoryRepository _repository;

    public ExpenseCategoryService(IExpenseCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExpenseCategory> CreateAsync(User user, CreateExpenseCategoryRequest request)
    {
        var expenseCategory = new ExpenseCategory();
        expenseCategory.Name = request.Name;
        expenseCategory.Icon = request.Icon;
        expenseCategory.IconBg = request.IconBg;
        expenseCategory.IconColor = request.IconColor;
        expenseCategory.UserId = user.Id;

        await _repository.CreateAsync(expenseCategory);
        return expenseCategory;
    }
    public async Task<ExpenseCategory> GetOneAsync(User user, Guid id)
    {
        var data = await _repository.GetOneAsync(user, id);

        if (data == null)
            throw new ModelNotFoundException("Expense category not found");
        return data;
    }
    public async Task<PaginatedListResponse<ExpenseCategory>> GetAllAsync(User user, int pageIndex, int pageSize)
    {
        var count = await _repository.CountAsync(user);
        var responseData = await _repository.GetAllAsync(user, pageIndex, pageSize);
        return PaginatedListResponse<ExpenseCategory>.Create(responseData, pageIndex, pageSize, count);
    }
    public async Task<ExpenseCategory> UpdateAsync(User user, Guid id, UpdateExpenseCategoryRequest request)
    {
        var expenseCategory = await _repository.GetOneAsync(user, id);

        if (expenseCategory == null)
            throw new ModelNotFoundException("Expense category not found");

        if (!string.IsNullOrEmpty(request.Name))
            expenseCategory.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Icon))
            expenseCategory.Icon = request.Icon;

        if (!string.IsNullOrEmpty(request.IconBg))
            expenseCategory.IconBg = request.IconBg;

        if (!string.IsNullOrEmpty(request.IconColor))
            expenseCategory.IconColor = request.IconColor;

        await _repository.UpdateAsync(expenseCategory);

        return expenseCategory;
    }
    public async Task DeleteAsync(User user, Guid id)
    {
        var expenseCategory = await _repository.GetOneAsync(user, id);
        if (expenseCategory == null)
            throw new ModelNotFoundException("Expense category not found");
        await _repository.DeleteAsync(expenseCategory);
    }
}
