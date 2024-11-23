using System;
using FinancasApp.Data;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Repositories;

public class ExpenseCategoryRepository : IExpenseCategoryRepository
{
    private readonly AppDbContext _context;

    public ExpenseCategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ExpenseCategory> CreateAsync(ExpenseCategory expenseCategory)
    {
        await _context.ExpenseCategories.AddAsync(expenseCategory);
        await _context.SaveChangesAsync();
        return expenseCategory;
    }
    public async Task<ExpenseCategory?> GetOneAsync(User user, Guid id)
    {
        return await _context.ExpenseCategories.FirstOrDefaultAsync(
           x => x.Id == id && x.UserId == user.Id
       );
    }
    public async Task<int> CountAsync(User user)
    {
        var count = await _context.ExpenseCategories
           .Where(b => b.UserId.Equals(user.Id))
           .CountAsync();

        return count;
    }
    public async Task<List<ExpenseCategory>> GetAllAsync(User user, int pageIndex, int pageSize)
    {
        var data = await _context.ExpenseCategories
           .AsNoTracking()
           .Where(b => b.UserId.Equals(user.Id))
           .OrderBy(b => b.Id)
           .Skip((pageIndex - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();

        var responseData = data.Select(d => d).ToList();
        return responseData;
    }
    public async Task<ExpenseCategory> UpdateAsync(ExpenseCategory expenseCategory)
    {
        _context.ExpenseCategories.Update(expenseCategory);
        await _context.SaveChangesAsync();
        return expenseCategory;
    }
    public async Task DeleteAsync(ExpenseCategory expenseCategory)
    {
        _context.ExpenseCategories.Remove(expenseCategory);
        await _context.SaveChangesAsync();
    }
}
