using System;
using FinancasApp.Models;

namespace FinancasApp.Controllers.V1.Dtos.Response;

public class EntryResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid? IncomeCategoryId { get; set; }
    public IncomeCategory? IncomeCategory { get; set; }
    public Guid? ExpenseCategoryId { get; set; }
    public ExpenseCategory? ExpenseCategory { get; set; }
    public Guid BankAccountId { get; set; }
    public string UserId { get; set; }
    public float Price { get; set; }
    public string? Note { get; set; }
    public bool? Payed { get; set; }
    public DateTime? DateWhenPayed { get; set; }
    public string EntryType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public EntryResponse(Entry entry)
    {
        Id = entry.Id;
        Title = entry.Title;
        IncomeCategoryId = entry.IncomeCategoryId;
        IncomeCategory = entry.IncomeCategory;
        ExpenseCategoryId = entry.ExpenseCategoryId;
        ExpenseCategory = entry.ExpenseCategory;
        BankAccountId = entry.BankAccountId;
        UserId = entry.UserId;
        Price = entry.Price;
        Note = entry.Note;
        Payed = entry.Payed;
        DateWhenPayed = entry.DateWhenPayed;
        CreatedAt = entry.CreatedAt;
        UpdatedAt = entry.UpdatedAt;
        if(entry.EntryType == 0){
            EntryType = FinancasApp.Enums.EntryType.EXPENSE.ToString();
        }else{
            EntryType = FinancasApp.Enums.EntryType.INCOME.ToString();
        }
    }
}
