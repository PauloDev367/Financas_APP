using System;
using FinancasApp.Models;

namespace FinancasApp.Repositories.Ports;

public interface IBankAccountRepository
{
    public Task<BankAccount> CreateAsync(BankAccount bankAccount);
    public Task<BankAccount> UpdateAsync(BankAccount bankAccount);
    public Task DeleteAsync(BankAccount bankAccount);
    public Task<BankAccount?> GetOneAsync(User user, Guid bankAccountId);
    public Task<List<BankAccount>> GetAllPaginatedAsync(int pageIndex, int pageSize, User user);
    public Task<int> CountTotalAsync(User user);
    public Task<bool> BankAccountExistsAsync(User user, string bankName);
}
