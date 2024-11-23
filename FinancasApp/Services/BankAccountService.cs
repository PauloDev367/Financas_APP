using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Controllers.V1.Dtos.Response;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;

namespace FinancasApp.Services;

public class BankAccountService
{
    private readonly IBankAccountRepository _repository;

    public BankAccountService(IBankAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreatedBankAccountResponse> CreateAsync(User user, CreateBankAccountRequest request)
    {
        if (await _repository.BankAccountExistsAsync(user, request.Name))
            throw new DuplicatedAccountBankException("Do you already have an account with this name!");

        var bankAccount = new BankAccount();
        bankAccount.Name = request.Name;
        bankAccount.Balance = request.Balance;
        bankAccount.UserId = user.Id;

        await _repository.CreateAsync(bankAccount);

        return new CreatedBankAccountResponse(bankAccount);
    }

    public async Task<CreatedBankAccountResponse> UpdateAsync(User user, UpdateBankAccountRequest request, Guid bankAccountId)
    {
        var bankAccount = await _repository.GetOneAsync(user, bankAccountId);
        if (request.Name != bankAccount.Name)
        {
            if (await _repository.BankAccountExistsAsync(user, request.Name))
                throw new DuplicatedAccountBankException("Do you already have an account with this name!");
        }

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        if (!string.IsNullOrEmpty(request.Name))
            bankAccount.Name = request.Name;

        if (request.Balance.HasValue)
            bankAccount.Balance = request.Balance.Value;

        await _repository.UpdateAsync(bankAccount);
        return new CreatedBankAccountResponse(bankAccount);
    }

    public async Task DeleteAsync(User user, Guid bankAccountId)
    {
        var bankAccount = await _repository.GetOneAsync(user, bankAccountId);

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        await _repository.DeleteAsync(bankAccount);
    }

    public async Task<CreatedBankAccountResponse> GetOneAsync(User user, Guid bankAccountId)
    {
        var bankAccount = await _repository.GetOneAsync(user, bankAccountId);

        if (bankAccount == null)
            throw new ModelNotFoundException("Bank account not founded");

        return new CreatedBankAccountResponse(bankAccount);
    }

    public async Task<PaginatedListResponse<CreatedBankAccountResponse>> GetAllPaginatedAsync(int pageIndex, int pageSize, User user)
    {
        var count = await _repository.CountTotalAsync(user);
        var players = await _repository.GetAllPaginatedAsync(pageIndex, pageSize, user);

        var data = players.Select(x => new CreatedBankAccountResponse(x)).ToList();

        return PaginatedListResponse<CreatedBankAccountResponse>.Create(data, pageIndex, pageSize, count);
    }

}
