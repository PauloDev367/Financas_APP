using System;

namespace FinancasApp.Models;

public class BankAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Balance { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
