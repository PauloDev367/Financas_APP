using System;

namespace FinancasApp.Controllers.V1.Dtos.Response;

public class ResponseBase<T>
{
    public T? Success { get; set; }
    public List<string>? Errors { get; set; }
    public ResponseBase()
    {
        Errors = new List<string>();
    }
    public void AddError(string error) => Errors.Add(error);
    public void SetErros(IEnumerable<string> erros) => Errors.AddRange(erros);

}
