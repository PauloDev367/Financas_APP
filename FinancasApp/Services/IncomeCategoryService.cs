using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Data;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Services;

public class IncomeCategoryService
{
    private readonly AppDbContext _context;
    public IncomeCategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IncomeCategory> CreateAsync(User user, CreateIncomeCategoryRequest request)
    {
        var incomeCategory = new IncomeCategory();
        incomeCategory.Name = request.Name;
        incomeCategory.Icon = request.Icon;
        incomeCategory.IconBg = request.IconBg;
        incomeCategory.IconColor = request.IconColor;
        incomeCategory.UserId = user.Id;

        await _context.IncomeCategories.AddAsync(incomeCategory);
        await _context.SaveChangesAsync();

        return incomeCategory;
    }
    public async Task<IncomeCategory> GetOneAsync(User user, Guid id)
    {
        var data = await _context.IncomeCategories.FirstOrDefaultAsync(
            x => x.Id == id && x.UserId == user.Id
        );

        if (data == null)
        {
            throw new ModelNotFoundException("Income category not found");
        }
        return data;
    }
    public async Task<PaginatedListResponse<IncomeCategory>> GetAllAsync(User user, int pageIndex, int pageSize)
    {
        var count = await _context.IncomeCategories
            .Where(b => b.UserId.Equals(user.Id))
            .CountAsync();

        var data = await _context.IncomeCategories
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var responseData = data.Select(d => d).ToList();
        return PaginatedListResponse<IncomeCategory>.Create(responseData, pageIndex, pageSize, count);
    }
    public async Task<IncomeCategory> UpdateAsync(User user, Guid id, UpdateIncomeCategoryRequest request)
    {
        var incomeCategory = await _context.IncomeCategories.FirstOrDefaultAsync(
                   x => x.Id == id && x.UserId == user.Id
               );

        if (incomeCategory == null)
        {
            throw new ModelNotFoundException("Income category not found");
        }

        if (!string.IsNullOrEmpty(request.Name))
            incomeCategory.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Icon))
            incomeCategory.Icon = request.Icon;

        if (!string.IsNullOrEmpty(request.IconBg))
            incomeCategory.IconBg = request.IconBg;

        if (!string.IsNullOrEmpty(request.IconColor))
            incomeCategory.IconColor = request.IconColor;

        _context.IncomeCategories.Update(incomeCategory);
        await _context.SaveChangesAsync();

        return incomeCategory;
    }
    public async Task DeleteAsync(User user, Guid id)
    {
        var incomeCategory = await _context.IncomeCategories.FirstOrDefaultAsync(
                x => x.Id == id && x.UserId == user.Id
            );

        _context.IncomeCategories.Remove(incomeCategory);
        await _context.SaveChangesAsync();
    }
}
