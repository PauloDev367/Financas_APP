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
    private readonly UserManager<User> _userManager;
    public ExpenseCategoryService(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<ExpenseCategory> CreateAsync(string userEmail, CreateExpenseCategoryRequest request)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

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
    public async Task<ExpenseCategory> GetOneAsync(string userEmail, Guid id)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var data = await _context.ExpenseCategories.FirstOrDefaultAsync(
            x => x.Id == id && x.UserId == user.Id
        );

        if (data == null)
        {
            throw new ModelNotFoundException("Expense category not found");
        }
        return data;
    }
    public async Task<PaginatedListResponse<ExpenseCategory>> GetAllAsync(string userEmail, int pageIndex, int pageSize)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

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
    public async Task<ExpenseCategory> UpdateAsync(string userEmail, Guid id, UpdateExpenseCategoryRequest request)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

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
    public async Task DeleteAsync(string userEmail, Guid id)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var expenseCategory = await _context.ExpenseCategories.FirstOrDefaultAsync(
                x => x.Id == id && x.UserId == user.Id
            );

        _context.ExpenseCategories.Remove(expenseCategory);
        await _context.SaveChangesAsync();
    }
}
