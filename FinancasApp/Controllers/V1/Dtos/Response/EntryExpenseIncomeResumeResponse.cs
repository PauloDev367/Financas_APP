namespace FinancasApp.Controllers.V1.Dtos.Response;

public record class EntryExpenseIncomeResumeResponse
{
    public int TotalExpense { get; set; }
    public int TotalIncome { get; set; }
}
