using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Data;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Services;

public class IncomeCategoryService
{
    private readonly IIncomeCategoryRepository _repository;

    public IncomeCategoryService(IIncomeCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IncomeCategory> CreateAsync(User user, CreateIncomeCategoryRequest request)
    {
        var incomeCategory = new IncomeCategory();
        incomeCategory.Name = request.Name;
        incomeCategory.Icon = request.Icon;
        incomeCategory.IconBg = request.IconBg;
        incomeCategory.IconColor = request.IconColor;
        incomeCategory.UserId = user.Id;

        await _repository.CreateAsync(incomeCategory);

        return incomeCategory;
    }
    public async Task<IncomeCategory> GetOneAsync(User user, Guid id)
    {
        var data = await _repository.GetOneAsync(user, id);
        if (data == null)
            throw new ModelNotFoundException("Income category not found");
        return data;
    }
    public async Task<PaginatedListResponse<IncomeCategory>> GetAllAsync(User user, int pageIndex, int pageSize)
    {
        var count = await _repository.CountAsync(user);
        var responseData = await _repository.GetAllAsync(user, pageIndex, pageSize);
        return PaginatedListResponse<IncomeCategory>.Create(responseData, pageIndex, pageSize, count);
    }
    public async Task<IncomeCategory> UpdateAsync(User user, Guid id, UpdateIncomeCategoryRequest request)
    {
        var incomeCategory = await _repository.GetOneAsync(user, id);
        if (incomeCategory == null)
            throw new ModelNotFoundException("Income category not found");

        if (!string.IsNullOrEmpty(request.Name))
            incomeCategory.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Icon))
            incomeCategory.Icon = request.Icon;

        if (!string.IsNullOrEmpty(request.IconBg))
            incomeCategory.IconBg = request.IconBg;

        if (!string.IsNullOrEmpty(request.IconColor))
            incomeCategory.IconColor = request.IconColor;

        await _repository.UpdateAsync(incomeCategory);

        return incomeCategory;
    }
    public async Task DeleteAsync(User user, Guid id)
    {
        var incomeCategory = await _repository.GetOneAsync(user, id);
        if (incomeCategory == null)
            throw new ModelNotFoundException("Income category not found");
        await _repository.DeleteAsync(incomeCategory);
    }
}
