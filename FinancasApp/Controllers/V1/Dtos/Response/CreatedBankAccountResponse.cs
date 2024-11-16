using FinancasApp.Models;

namespace FinancasApp.Controllers.V1.Dtos.Response;

public record class CreatedBankAccountResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public CreatedBankAccountResponse(BankAccount bankAccount)
    {
        Id = bankAccount.Id;
        Name = bankAccount.Name;
        Balance = bankAccount.Balance;
        CreatedAt = bankAccount.CreatedAt;
        UpdatedAt = bankAccount.UpdatedAt;
    }
}
