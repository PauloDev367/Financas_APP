using System;

namespace FinancasApp.Models;

public class ExpenseCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string IconBg { get; set; }
    public string IconColor { get; set; }
    public string UserId { get; set; }
    public List<Entry> Entries { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
