using System;

namespace FinancasApp.Models;

public class IncomeCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string IconBg { get; set; }
    public string IconColor { get; set; }
    public Guid UserId { get; set; }
    public List<Entry> Entries { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
