using System;
using FinancasApp.Enums;

namespace FinancasApp.Models;

public class Entry
{
    public Guid Id { get; set; }
    public Guid? IncomeCategoryId { get; set; }
    public IncomeCategory? IncomeCategory { get; set; }
    public Guid? ExpenseCategoryId { get; set; }
    public ExpenseCategory? ExpenseCategory { get; set; }
    public Guid BankAccountId { get; set; }
    public string UserId { get; set; }
    public string? Note { get; set; }
    public bool? Payed { get; set; }
    public DateTime? DateWhenPayed { get; set; }
    public EntryType EntryType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
