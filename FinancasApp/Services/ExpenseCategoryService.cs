using System;
using FinancasApp.Data;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using Microsoft.AspNetCore.Identity;


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

    public async Task<ExpenseCategory> CreateAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        
    }
    public async Task<ExpenseCategory> GetOneAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");


    }
    public async Task<List<ExpenseCategory>> GetAllAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");


    }
    public async Task<ExpenseCategory> UpdateAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");


    }
    public async Task DeleteAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");


    }
}
