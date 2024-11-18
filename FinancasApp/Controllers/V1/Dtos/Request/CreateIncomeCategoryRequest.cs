using System;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class CreateIncomeCategoryRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Icon { get; set; }
    [Required]
    public string IconBg { get; set; }
    [Required]
    public string IconColor { get; set; }
}
