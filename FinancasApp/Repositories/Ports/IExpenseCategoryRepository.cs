using System;
using FinancasApp.Models;

namespace FinancasApp.Repositories.Ports;

public interface IExpenseCategoryRepository
{
    public Task<ExpenseCategory> CreateAsync(ExpenseCategory expenseCategory);
    public Task<ExpenseCategory?> GetOneAsync(User user, Guid id);
    public Task<int> CountAsync(User user);
    public Task<List<ExpenseCategory>> GetAllAsync(User user, int pageIndex, int pageSize);
    public Task<ExpenseCategory> UpdateAsync(ExpenseCategory expenseCategory);
    public Task DeleteAsync(ExpenseCategory expenseCategory);
}
