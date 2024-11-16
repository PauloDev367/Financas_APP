using System;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class UserLoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
