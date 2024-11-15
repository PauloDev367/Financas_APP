using System;
using FinancasApp.Data.Configuration;
using FinancasApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FinancasApp.Data;

public class AppDbContext : IdentityDbContext
{
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<IncomeCategory> IncomeCategories { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BankAccountConf());
        modelBuilder.ApplyConfiguration(new EntryConf());
        modelBuilder.ApplyConfiguration(new ExpenseCategoryConf());
        modelBuilder.ApplyConfiguration(new IncomeCategoryConf());
        modelBuilder.ApplyConfiguration(new UserConf());
    }
}
