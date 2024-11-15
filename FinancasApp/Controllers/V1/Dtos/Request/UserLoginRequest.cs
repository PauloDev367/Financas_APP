using System;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class UserLoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }
}
