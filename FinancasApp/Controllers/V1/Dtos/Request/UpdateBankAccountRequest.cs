namespace FinancasApp.Controllers.V1.Dtos.Request;

public record class UpdateBankAccountRequest
{
    public string? Name { get; set; }
    public float? Balance { get; set; }
}
