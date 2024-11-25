using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Enums;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;

namespace FinancasApp.Services;

public class EntryService
{
    private readonly IEntryRepository _repository;

    public EntryService(IEntryRepository repository)
    {
        _repository = repository;
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
            DateWhenPayed = request.DataWhenPayed,
            Price = request.Price
        };

        if (finalEntryType == EntryType.INCOME)
        {
            entry.IncomeCategoryId = request.IncomeCategoryId;
        }
        else
        {
            entry.ExpenseCategoryId = request.ExpenseCategoryId;
        }

        await _repository.CreateAsync(entry);
        return entry;
    }
    public async Task<Entry> GetOneAsync(User user, Guid id)
    {
        var data = await _repository.GetOneAsync(user, id);

        if (data == null)
            throw new ModelNotFoundException("Entry not found");

        return data;
    }
    public async Task<PaginatedListResponse<EntryResponse>> GetAllAsync(User user, Guid bankAccountId, int pageIndex, int pageSize, int? year, int? month)
    {
        var count = await _repository.CountAsync(user.Id);
        var responseData = await _repository.GetAllPaginateAsync(user, bankAccountId, pageIndex, pageSize, year, month);
        var final = responseData.Select(r => new EntryResponse(r)).ToList();

        return PaginatedListResponse<EntryResponse>.Create(final, pageIndex, pageSize, count);
    }
    public async Task<List<TotalPerCategoryResponse>> GetTotalByCategoriesAsync(User user, Guid bankAccountId, int? year, int? month)
    {
        var data = await _repository.GetTotalByCategoriesAsync(user, bankAccountId, year, month);
        return data;
    }
    public async Task<EntryExpenseIncomeResumeResponse> GetEntryExpenseIncomeResumeAsync(User user, Guid bankAccountId, int? year, int? month)
    {
        var totalExpense = await _repository.CountByEntryTypeAsync(user, bankAccountId, EntryType.EXPENSE, year, month);
        var totalIncome = await _repository.CountByEntryTypeAsync(user, bankAccountId, EntryType.INCOME, year, month);

        var response = new EntryExpenseIncomeResumeResponse { TotalExpense = totalExpense, TotalIncome = totalIncome };
        return response;
    }
    public async Task DeleteAsync(User user, Guid id)
    {
        var data = await _repository.GetOneAsync(user, id);

        if (data == null)
            throw new ModelNotFoundException("Entry not found");

        await _repository.DeleteAsync(data);
    }
    public async Task<Entry> ChangeReceivedStatusAsync(User user, Guid id, UpdateEntryReceivedStatusRequest request)
    {
        var entry = await _repository.GetOneAsync(user, id);

        if (entry == null)
            throw new ModelNotFoundException("Entry not found");

        entry.Payed = request.Payed;
        if (request.Payed == false && request.DateWhenPayed == null)
        {
            entry.DateWhenPayed = null;
        }
        else
        {
            entry.DateWhenPayed = request.DateWhenPayed;
        }
        await _repository.UpdateAsync(entry);

        return entry;
    }
    public async Task<Entry> UpdateEntryTypeAsync(User user, Guid id, UpdateEntryTypeRequest request)
    {
        var entry = await _repository.GetOneAsync(user, id);

        if (entry == null)
            throw new ModelNotFoundException("Entry not found");

        var entryType = request.EntryType;
        EntryType finalEntryType = EntryType.EXPENSE;

        if (entryType == EntryType.EXPENSE.ToString())
        {
            entry.ExpenseCategoryId = request.ExpenseCategoryId;
            entry.IncomeCategoryId = null;
            finalEntryType = EntryType.EXPENSE;
        }
        if (entryType == EntryType.INCOME.ToString())
        {
            entry.IncomeCategoryId = request.IncomeCategoryId;
            entry.ExpenseCategoryId = null;
            finalEntryType = EntryType.INCOME;
        }

        entry.EntryType = finalEntryType;

        await _repository.UpdateAsync(entry);
        return entry;
    }
    public async Task<Entry> UpdateAsync(User user, Guid id, UpdateEntryRequest request)
    {
        var entry = await _repository.GetOneAsync(user, id);

        if (entry == null)
        {
            throw new ModelNotFoundException("Entry not found");
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            entry.Title = request.Title;
        }
        if (request.BankAccountId != null)
        {
            entry.BankAccountId = (Guid)request.BankAccountId;
        }
        if (!string.IsNullOrEmpty(request.Note))
        {
            entry.Note = request.Note;
        }

        await _repository.UpdateAsync(entry);
        return entry;
    }

}
