using System;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class UpdateIncomeCategoryRequest
{
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public string? IconBg { get; set; }
    public string? IconColor { get; set; }
}
