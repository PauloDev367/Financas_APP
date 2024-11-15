using System;
using FinancasApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Extensions;

public static class ConfigureDbContextExtension
{
    public static void ConfigureDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string is not configured");

        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}
