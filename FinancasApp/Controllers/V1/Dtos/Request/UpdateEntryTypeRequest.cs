using System;
using System.ComponentModel.DataAnnotations;
using FinancasApp.Attributes;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class UpdateEntryTypeRequest
{
    [Required]
    public string EntryType { get; init; }

    [RequiredIf("EntryType", "INCOME")]
    public Guid? IncomeCategoryId { get; init; }

    [RequiredIf("EntryType", "EXPENSE")]
    public Guid? ExpenseCategoryId { get; init; }
}
