using System;
using FinancasApp.Models;

namespace FinancasApp.Configurations;

public class RequestUser
{
    public string Id { get; set; }
    public string Email { get; set; }
    public User User { get; set; }
}
