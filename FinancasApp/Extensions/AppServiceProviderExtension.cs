using System;
using FinancasApp.Services;

namespace FinancasApp.Extensions;

public static class AppServiceProviderExtension
{
    public static void LoadDependencies(this IServiceCollection service)
    {
        service.AddTransient<IdentityService, IdentityService>();
        service.AddScoped<BankAccountService>();
        service.AddScoped<ExpenseCategoryService>();
        service.AddScoped<IncomeCategoryService>();

    }

}
