using System;
using FinancasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<IncomeCategory> IncomeCategories { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
