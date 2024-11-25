namespace FinancasApp.Controllers.V1.Dtos.Response;

public record class TotalPerCategoryResponse
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public string IconBg { get; set; }
    public string IconColor { get; set; }
    public float Total { get; set; }
    public string CategoryType { get; set; }
}
