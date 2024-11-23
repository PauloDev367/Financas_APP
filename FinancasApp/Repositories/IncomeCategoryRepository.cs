using System;
using FinancasApp.Data;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Repositories;

public class IncomeCategoryRepository : IIncomeCategoryRepository
{
    private readonly AppDbContext _context;

    public IncomeCategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IncomeCategory> CreateAsync(IncomeCategory incomeCategory)
    {
        await _context.IncomeCategories.AddAsync(incomeCategory);
        await _context.SaveChangesAsync();
        return incomeCategory;
    }
    public async Task<IncomeCategory?> GetOneAsync(User user, Guid id)
    {
        return await _context.IncomeCategories.FirstOrDefaultAsync(
           x => x.Id == id && x.UserId == user.Id
       );
    }
    public async Task<int> CountAsync(User user)
    {
        var count = await _context.IncomeCategories
           .Where(b => b.UserId.Equals(user.Id))
           .CountAsync();

        return count;
    }
    public async Task<List<IncomeCategory>> GetAllAsync(User user, int pageIndex, int pageSize)
    {
        var data = await _context.IncomeCategories
           .AsNoTracking()
           .Where(b => b.UserId.Equals(user.Id))
           .OrderBy(b => b.Id)
           .Skip((pageIndex - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();

        var responseData = data.Select(d => d).ToList();
        return responseData;
    }
    public async Task<IncomeCategory> UpdateAsync(IncomeCategory incomeCategory)
    {
        _context.IncomeCategories.Update(incomeCategory);
        await _context.SaveChangesAsync();
        return incomeCategory;
    }
    public async Task DeleteAsync(IncomeCategory incomeCategory)
    {
        _context.IncomeCategories.Remove(incomeCategory);
        await _context.SaveChangesAsync();
    }
}
