using System;
using FinancasApp.Models;

namespace FinancasApp.Repositories.Ports;

public interface IIncomeCategoryRepository
{
    public Task<IncomeCategory> CreateAsync(IncomeCategory incomeCategory);
    public Task<IncomeCategory?> GetOneAsync(User user, Guid id);
    public Task<int> CountAsync(User user);
    public Task<List<IncomeCategory>> GetAllAsync(User user, int pageIndex, int pageSize);
    public Task<IncomeCategory> UpdateAsync(IncomeCategory incomeCategory);
    public Task DeleteAsync(IncomeCategory incomeCategory);
}
