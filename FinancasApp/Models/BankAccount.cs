using System;

namespace FinancasApp.Models;

public class BankAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Balance { get; set; }
    public Guid UserId { get; set; }

}
