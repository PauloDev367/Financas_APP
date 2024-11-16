using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public record class CreateBankAccountRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public float Balance { get; set; }
}
