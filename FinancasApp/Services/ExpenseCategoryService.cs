using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Data;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace FinancasApp.Services;

public class ExpenseCategoryService
{
    private readonly AppDbContext _context;

    public ExpenseCategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ExpenseCategory> CreateAsync(User user, CreateExpenseCategoryRequest request)
    {
        var expenseCategory = new ExpenseCategory();
        expenseCategory.Name = request.Name;
        expenseCategory.Icon = request.Icon;
        expenseCategory.IconBg = request.IconBg;
        expenseCategory.IconColor = request.IconColor;
        expenseCategory.UserId = user.Id;

        await _context.ExpenseCategories.AddAsync(expenseCategory);
        await _context.SaveChangesAsync();

        return expenseCategory;
    }
    public async Task<ExpenseCategory> GetOneAsync(User user, Guid id)
    {
        var data = await _context.ExpenseCategories.FirstOrDefaultAsync(
            x => x.Id == id && x.UserId == user.Id
        );

        if (data == null)
        {
            throw new ModelNotFoundException("Expense category not found");
        }
        return data;
    }
    public async Task<PaginatedListResponse<ExpenseCategory>> GetAllAsync(User user, int pageIndex, int pageSize)
    {
        var count = await _context.ExpenseCategories
            .Where(b => b.UserId.Equals(user.Id))
            .CountAsync();

        var data = await _context.ExpenseCategories
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var responseData = data.Select(d => d).ToList();
        return PaginatedListResponse<ExpenseCategory>.Create(responseData, pageIndex, pageSize, count);
    }
    public async Task<ExpenseCategory> UpdateAsync(User user, Guid id, UpdateExpenseCategoryRequest request)
    {
        var expenseCategory = await _context.ExpenseCategories.FirstOrDefaultAsync(
                   x => x.Id == id && x.UserId == user.Id
               );

        if (expenseCategory == null)
        {
            throw new ModelNotFoundException("Expense category not found");
        }

        if (!string.IsNullOrEmpty(request.Name))
            expenseCategory.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Icon))
            expenseCategory.Icon = request.Icon;

        if (!string.IsNullOrEmpty(request.IconBg))
            expenseCategory.IconBg = request.IconBg;

        if (!string.IsNullOrEmpty(request.IconColor))
            expenseCategory.IconColor = request.IconColor;

        _context.ExpenseCategories.Update(expenseCategory);
        await _context.SaveChangesAsync();

        return expenseCategory;
    }
    public async Task DeleteAsync(User user, Guid id)
    {
        var expenseCategory = await _context.ExpenseCategories.FirstOrDefaultAsync(
                x => x.Id == id && x.UserId == user.Id
            );

        _context.ExpenseCategories.Remove(expenseCategory);
        await _context.SaveChangesAsync();
    }
}
