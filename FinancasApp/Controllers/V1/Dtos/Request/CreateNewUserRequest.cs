using System;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class CreateNewUserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
