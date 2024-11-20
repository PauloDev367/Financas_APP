using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Data;
using FinancasApp.Enums;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Services;

public class EntryService
{
    private readonly AppDbContext _context;
    public EntryService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Entry> CreateAsync(User user, CreateEntryRequest request)
    {
        var entryType = request.EntryType;
        var finalEntryType = EntryType.EXPENSE;
        if (entryType != EntryType.EXPENSE.ToString() && entryType != EntryType.INCOME.ToString())
            throw new InvalidEntryTypeException("The entry type should be " + EntryType.EXPENSE.ToString() + " or " + EntryType.INCOME.ToString());

        if (entryType == EntryType.INCOME.ToString())
            finalEntryType = EntryType.INCOME;

        var entry = new Entry
        {
            UserId = user.Id,
            Title = request.Title,
            EntryType = finalEntryType,
            BankAccountId = request.BankAccountId,
            Note = request.Note,
            Payed = request.Payed,
            DateWhenPayed = request.DataWhenPayed
        };

        if (finalEntryType == EntryType.INCOME)
        {
            entry.IncomeCategoryId = request.IncomeCategoryId;
        }
        else
        {
            entry.ExpenseCategoryId = request.ExpenseCategoryId;
        }

        await _context.Entries.AddAsync(entry);
        await _context.SaveChangesAsync();
        return entry;
    }
    public async Task<Entry> GetOneAsync(User user, Guid id)
    {
        var data = await _context.Entries
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);

        if (data == null)
        {
            throw new ModelNotFoundException("Entry not found");
        }

        return data;
    }
    public async Task<PaginatedListResponse<Entry>> GetAllAsync(User user, int pageIndex, int pageSize)
    {
        var count = await _context.Entries
            .Where(b => b.UserId.Equals(user.Id))
            .CountAsync();

        var data = await _context.Entries
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var responseData = data.Select(d => d).ToList();
        return PaginatedListResponse<Entry>.Create(responseData, pageIndex, pageSize, count);
    }
    // public async Task<Entry> UpdateAsync(User user, Guid id, UpdateEntryRequest request)
    // {

    // }
    // public async Task DeleteAsync(User user, Guid id)
    // {

    // }
    // public async Task<Entry> ChangeReceivedStatusAsync(User user, Guid id, UpdateEntryReceivedStatusRequest request)
    // {

    // }
    // public async Task<Entry> UpdateEntryTypeAsync(User user, Guid id, UpdateEntryTypeRequest request)
    // {

    // }

}
