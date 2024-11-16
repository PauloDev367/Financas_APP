using System;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Data;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

namespace FinancasApp.Services;

public class BankAccountService
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public BankAccountService(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<CreatedBankAccountResponse> CreateAsync(string userEmail, CreateBankAccountRequest request)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        if (_context.BankAccounts.Any(b => b.Name == request.Name))
            throw new DuplicatedAccountBankException("Do you already have an account with this name!");

        var bankAccount = new BankAccount();
        bankAccount.Name = request.Name;
        bankAccount.Balance = request.Balance;
        bankAccount.UserId = user.Id;

        await _context.BankAccounts.AddAsync(bankAccount);
        await _context.SaveChangesAsync();
        return new CreatedBankAccountResponse(bankAccount);
    }

    public async Task<CreatedBankAccountResponse> UpdateAsync(string userEmail, UpdateBankAccountRequest request, Guid bankAccountId)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var bankAccount = await _context.BankAccounts
            .FirstOrDefaultAsync(x => x.Id.Equals(bankAccountId) && x.UserId.Equals(user.Id));

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        if (!string.IsNullOrEmpty(request.Name))
            bankAccount.Name = request.Name;

        if (request.Balance.HasValue)
            bankAccount.Balance = request.Balance.Value;

        _context.BankAccounts.Update(bankAccount);
        await _context.SaveChangesAsync();
        return new CreatedBankAccountResponse(bankAccount);
    }

    public async Task DeleteAsync(string userEmail, Guid bankAccountId)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var bankAccount = await _context.BankAccounts
                   .FirstOrDefaultAsync(x => x.Id.Equals(bankAccountId) && x.UserId.Equals(user.Id));

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        _context.BankAccounts.Remove(bankAccount);
        await _context.SaveChangesAsync();
    }

    public async Task<CreatedBankAccountResponse> GetOneAsync(string userEmail, Guid bankAccountId)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var bankAccount = await _context.BankAccounts
                   .FirstOrDefaultAsync(x => x.Id.Equals(bankAccountId) && x.UserId.Equals(user.Id));

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        return new CreatedBankAccountResponse(bankAccount);
    }

    public async Task<PaginatedListResponse<CreatedBankAccountResponse>> GetAllPaginatedAsync(int pageIndex, int pageSize, string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var count = await _context.BankAccounts
                                  .Where(b => b.UserId.Equals(user.Id))
                                  .CountAsync();

        var players = await _context.BankAccounts
            .AsNoTracking()
            .Where(b => b.UserId.Equals(user.Id))
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var data = players.Select(x => new CreatedBankAccountResponse(x)).ToList();

        return PaginatedListResponse<CreatedBankAccountResponse>.Create(data, pageIndex, pageSize, count);
    }

}
