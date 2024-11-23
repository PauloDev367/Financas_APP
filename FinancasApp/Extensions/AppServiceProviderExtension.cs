using System;
using FinancasApp.Configurations;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using FinancasApp.Repositories;
using FinancasApp.Repositories.Ports;
using FinancasApp.Services;
using Microsoft.AspNetCore.Identity;

namespace FinancasApp.Extensions;

public static class AppServiceProviderExtension
{
    public static void LoadDependencies(this IServiceCollection service)
    {
        service.AddHttpContextAccessor(); 
        service.AddTransient<IdentityService, IdentityService>();
        service.AddTransient<IBankAccountRepository, BankAccountRepository>();
        service.AddScoped<BankAccountService>();
        service.AddScoped<ExpenseCategoryService>();
        service.AddScoped<IncomeCategoryService>();
        service.AddScoped<EntryService>();


        service.AddScoped<RequestUser>(serviceProvider =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                var userEmail = httpContextAccessor.HttpContext?.User
                    .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                    throw new UnauthorizedActionException("Invalid user email in token");

                var user = userManager.FindByEmailAsync(userEmail).Result;
                if (user == null)
                    throw new UnauthorizedActionException("You don't have permission for this action");

                return new RequestUser
                {
                    Id = user.Id,
                    Email = user.Email,
                    User = user
                };
            });

    }

}
