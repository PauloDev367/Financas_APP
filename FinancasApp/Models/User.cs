using System;
using Microsoft.AspNetCore.Identity;

namespace FinancasApp.Models;

public class User : IdentityUser
{
    public List<BankAccount> BankAccounts { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
}
