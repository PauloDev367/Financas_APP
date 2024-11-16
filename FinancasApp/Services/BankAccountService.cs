using System;
using System.Data.Entity;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Data;
using FinancasApp.Exceptions;
using FinancasApp.Models;
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

    public async Task<BankAccount> CreateAsync(string userId, CreateBankAccountRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        if (_context.BankAccounts.Any(b => b.Name == request.Name))
            throw new DuplicatedAccountBankException("Do you already have an account with this name!");

        var bankAccount = new BankAccount();
        bankAccount.Name = request.Name;
        bankAccount.Balance = request.Balance;
        bankAccount.UserId = user.Id;

        await _context.BankAccounts.AddAsync(bankAccount);

        return bankAccount;
    }

    public async Task<BankAccount> UpdateAsync(string userId, UpdateBankAccountRequest request, Guid bankAccountId)
    {
        var user = await _userManager.FindByIdAsync(userId);
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
        return bankAccount;
    }

    public async Task DeleteAsync(string userId, Guid bankAccountId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var bankAccount = await _context.BankAccounts
                   .FirstOrDefaultAsync(x => x.Id.Equals(bankAccountId) && x.UserId.Equals(user.Id));

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        _context.BankAccounts.Remove(bankAccount);
    }

    public async Task<BankAccount> GetOneAsync(string userId, Guid bankAccountId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var bankAccount = await _context.BankAccounts
                   .FirstOrDefaultAsync(x => x.Id.Equals(bankAccountId) && x.UserId.Equals(user.Id));

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        return bankAccount;
    }

    public async Task<PaginatedListResponse<BankAccount>> GetAllPaginatedAsync(int pageIndex, int pageSize, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UnauthorizedActionException("You don't have the permission for this action");

        var players = await _context.BankAccounts
                    .AsNoTracking()
                    .OrderBy(b => b.Id)
                    .Where(b => b.UserId.Equals(user.Id))
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

        var count = await _context.BankAccounts.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return new PaginatedListResponse<BankAccount>(players, pageIndex, totalPages);
    }

}
