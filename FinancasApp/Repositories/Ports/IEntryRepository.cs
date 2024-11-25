using System;
using FinancasApp.Enums;
using FinancasApp.Models;

namespace FinancasApp.Repositories.Ports;

public interface IEntryRepository
{
    public Task<Entry> CreateAsync(Entry entry);
    public Task<Entry?> GetOneAsync(User user, Guid id);
    public Task<int> CountAsync(string userId);
    public Task<List<Entry>> GetAllPaginateAsync(User user, Guid bankAccountId, int pageIndex, int pageSize, int? year, int? month);
    public Task<int> CountByEntryTypeAsync(User user, Guid bankAccountId, EntryType entryType, int? year, int? month);
    public Task DeleteAsync(Entry entry);
    public Task<Entry> UpdateAsync(Entry entry);
}
