using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Data;
using FinancasApp.Enums;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Repositories;

public class EntryRepository : IEntryRepository
{
    private readonly AppDbContext _context;
    public EntryRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Entry> CreateAsync(Entry entry)
    {
        await _context.Entries.AddAsync(entry);
        await _context.SaveChangesAsync();
        return entry;
    }

    public async Task<Entry?> GetOneAsync(User user, Guid id)
    {
        var data = await _context.Entries
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
        return data;
    }

    public async Task<int> CountAsync(string userId)
    {
        return await _context.Entries
            .Where(b => b.UserId.Equals(userId))
            .CountAsync();
    }
    public async Task<List<Entry>> GetAllPaginateAsync(User user, Guid bankAccountId, int pageIndex, int pageSize, int? year, int? month)
    {
        var data = _context.Entries
            .AsNoTracking()
            .Include(x => x.ExpenseCategory)
            .Include(x => x.IncomeCategory)
            .Where(b => b.UserId.Equals(user.Id))
            .Where(b => b.BankAccountId == bankAccountId)
            .OrderByDescending(b => b.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        if (month != null)
        {
            data = data.Where(e => e.CreatedAt.Month == month);
        }
        if (year != null)
        {
            data = data.Where(e => e.CreatedAt.Year == year);
        }


        await data.ToListAsync();

        var responseData = data.Select(d => d).ToList();
        return responseData;
    }
    public async Task<int> CountByEntryTypeAsync(User user, Guid bankAccountId, EntryType entryType, int? year, int? month)
    {
        var data = _context.Entries
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .Where(b => b.EntryType == entryType)
            .Where(b => b.BankAccountId == bankAccountId);

        if (month != null)
        {
            data = data.Where(e => e.CreatedAt.Month == month);
        }
        if (year != null)
        {
            data = data.Where(e => e.CreatedAt.Year == year);
        }
        var total = await data.CountAsync();
        return total;
    }
    public async Task<List<TotalPerCategoryResponse>> GetTotalByCategoriesAsync(User user, Guid bankAccountId, int? year, int? month)
    {
        var expenseCategories = await _context.ExpenseCategories
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .ToListAsync();

        var incomeCategories = await _context.IncomeCategories
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .ToListAsync();

        var response = new List<TotalPerCategoryResponse>();

        foreach (var exp in expenseCategories)
        {
            var data = _context.Entries
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .Where(b => b.ExpenseCategoryId == exp.Id)
            .Where(b => b.BankAccountId == bankAccountId);

            if (month != null)
            {
                data = data.Where(e => e.CreatedAt.Month == month);
            }
            if (year != null)
            {
                data = data.Where(e => e.CreatedAt.Year == year);
            }
            var total = await data.CountAsync();
            var res = new TotalPerCategoryResponse
            {
                CategoryType = "EXPENSE",
                Icon = exp.Icon,
                IconBg = exp.IconBg,
                IconColor = exp.IconColor,
                Name = exp.Name,
                Total = total
            };
            response.Add(res);
        }

        foreach (var inc in incomeCategories)
        {
            var data = _context.Entries
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .Where(b => b.IncomeCategoryId == inc.Id)
            .Where(b => b.BankAccountId == bankAccountId);

            if (month != null)
            {
                data = data.Where(e => e.CreatedAt.Month == month);
            }
            if (year != null)
            {
                data = data.Where(e => e.CreatedAt.Year == year);
            }
            var total = await data.CountAsync();
            var res = new TotalPerCategoryResponse
            {
                CategoryType = "INCOME",
                Icon = inc.Icon,
                IconBg = inc.IconBg,
                IconColor = inc.IconColor,
                Name = inc.Name,
                Total = total
            };
            response.Add(res);
        }

        return response;
    }
    public async Task DeleteAsync(Entry entry)
    {
        _context.Entries.Remove(entry);
        await _context.SaveChangesAsync();
    }
    public async Task<Entry> UpdateAsync(Entry entry)
    {

        _context.Entries.Update(entry);
        await _context.SaveChangesAsync();
        return entry;
    }
}
