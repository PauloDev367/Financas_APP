using System;
using System.ComponentModel.DataAnnotations;
using FinancasApp.Attributes;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class CreateEntryRequest
{
    [Required]
    [StringLength(100)]
    public string EntryType { get; init; }

    [RequiredIf("EntryType", "INCOME")]
    public Guid? IncomeCategoryId { get; init; }

    [RequiredIf("EntryType", "EXPENSE")]
    public Guid? ExpenseCategoryId { get; init; }

    [Required]
    [StringLength(255)]
    public string Title { get; init; }

    [Required]
    public Guid BankAccountId { get; init; }

    public string? Note { get; init; }

    [Required]
    public bool Payed { get; init; }

    [RequiredIf("Payed", true)]
    [DataType(DataType.Date)]
    public DateTime? DataWhenPayed { get; init; }
}
