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
    private readonly UserManager<User> _userManager;
    public IncomeCategoryService(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IncomeCategory> CreateAsync(string userEmail, CreateIncomeCategoryRequest request)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

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
    public async Task<IncomeCategory> GetOneAsync(string userEmail, Guid id)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var data = await _context.IncomeCategories.FirstOrDefaultAsync(
            x => x.Id == id && x.UserId == user.Id
        );

        if (data == null)
        {
            throw new ModelNotFoundException("Income category not found");
        }
        return data;
    }
    public async Task<PaginatedListResponse<IncomeCategory>> GetAllAsync(string userEmail, int pageIndex, int pageSize)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

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
    public async Task<IncomeCategory> UpdateAsync(string userEmail, Guid id, UpdateIncomeCategoryRequest request)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

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
    public async Task DeleteAsync(string userEmail, Guid id)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var incomeCategory = await _context.IncomeCategories.FirstOrDefaultAsync(
                x => x.Id == id && x.UserId == user.Id
            );

        _context.IncomeCategories.Remove(incomeCategory);
        await _context.SaveChangesAsync();
    }
}
