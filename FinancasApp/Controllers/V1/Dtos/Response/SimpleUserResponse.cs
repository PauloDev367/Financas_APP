using System;

namespace FinancasApp.Controllers.V1.Dtos.Response;

public class SimpleUserResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public SimpleUserResponse(Models.User identityUser)
    {
        Id = identityUser.Id.ToString();
        Email = identityUser.Email;
    }
}
