using System;
using System.ComponentModel.DataAnnotations;
using FinancasApp.Attributes;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class UpdateEntryTypeRequest
{
    [Required]
    public string EntryType { get; init; }

    [RequiredIf("EntryType", "INCOME")]
    public int? IncomeCategoryId { get; init; }

    [RequiredIf("EntryType", "EXPENSE")]
    public int? ExpenseCategoryId { get; init; }
}