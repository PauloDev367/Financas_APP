using FinancasApp.Data;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly AppDbContext _context;

    public BankAccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BankAccount> CreateAsync(BankAccount bankAccount)
    {
        await _context.BankAccounts.AddAsync(bankAccount);
        await _context.SaveChangesAsync();

        return bankAccount;
    }

    public async Task<bool> BankAccountExistsAsync(User user, string bankName)
    {
        return _context.BankAccounts.Any(b => b.Name == bankName && b.UserId == user.Id);
    }

    public async Task<BankAccount> UpdateAsync(BankAccount bankAccount)
    {
        _context.BankAccounts.Update(bankAccount);
        await _context.SaveChangesAsync();
        return bankAccount;
    }

    public async Task DeleteAsync(BankAccount bankAccount)
    {
        _context.BankAccounts.Remove(bankAccount);
        await _context.SaveChangesAsync();
    }

    public async Task<BankAccount?> GetOneAsync(User user, Guid bankAccountId)
    {
        var bankAccount = await _context.BankAccounts
            .FirstOrDefaultAsync(x => x.Id.Equals(bankAccountId) && x.UserId.Equals(user.Id));

        return bankAccount;
    }

    public async Task<List<BankAccount>> GetAllPaginatedAsync(int pageIndex, int pageSize, User user)
    {
        var bankAccounts = await _context.BankAccounts
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return bankAccounts;
    }

    public async Task<int> CountTotalAsync(User user)
    {
        var count = await _context.BankAccounts
            .Where(b => b.UserId.Equals(user.Id))
            .CountAsync();
        return count;
    }
}
