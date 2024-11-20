using System;
using System.ComponentModel.DataAnnotations;
using FinancasApp.Attributes;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class UpdateEntryRequest
{
    [StringLength(255)]
    public string? Title { get; init; }

    public Guid? BankAccountId { get; init; }

    public string? Note { get; init; }

}
