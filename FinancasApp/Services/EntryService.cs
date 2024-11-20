using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Data;
using FinancasApp.Enums;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FinancasApp.Services;

public class EntryService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    public EntryService(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    // public async Task<Entry> CreateAsync(string userEmail, CreateEntryRequest request)
    // {
    //     var entryEtype = request.EntryType;
    //     if (entryEtype != EntryType.EXPENSE.ToString() && entryEtype != EntryType.INCOME.ToString())
    //         throw new InvalidEntryTypeException("The entry type should be " + EntryType.EXPENSE.ToString() + " or " + EntryType.INCOME.ToString());

    //     var entry = new Entry();
        

    // }
    // public async Task<Entry> UpdateAsync(string userEmail, Guid id, UpdateEntryRequest request)
    // {

    // }
    // public async Task DeleteAsync(string userEmail, Guid id)
    // {

    // }
    // public async Task<Entry> ChangeReceivedStatusAsync(string userEmail, Guid id, UpdateEntryReceivedStatusRequest request)
    // {

    // }
    // public async Task<Entry> UpdateEntryTypeAsync(string userEmail, Guid id, UpdateEntryTypeRequest request)
    // {

    // }
    // public async Task<Entry> GetOneAsync(string userEmail, Guid id)
    // {

    // }
    // public async Task<List<Entry>> GetAllAsync(string userEmail, int pageIndex, int pageSize)
    // {

    // }
}
