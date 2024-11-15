using System;
using FinancasApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasApp.Data.Configuration;

public class IncomeCategoryConf : IEntityTypeConfiguration<IncomeCategory>
{
    public void Configure(EntityTypeBuilder<IncomeCategory> builder)
    {
        builder.ToTable("income_categories");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Icon)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.IconBg)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.IconColor)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(x => x.Entries)
            .WithOne(e => e.IncomeCategory)
            .HasForeignKey(e => e.IncomeCategoryId);

        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();
    }
}
