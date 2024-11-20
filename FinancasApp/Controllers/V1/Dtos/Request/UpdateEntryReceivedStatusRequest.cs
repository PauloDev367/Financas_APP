using System;
using System.ComponentModel.DataAnnotations;
using FinancasApp.Attributes;

namespace FinancasApp.Controllers.V1.Dtos.Request;

public class UpdateEntryReceivedStatusRequest
{
    [Required]
    public bool Payed { get; init; }

    [RequiredIf("Payed", true)]
    [DataType(DataType.Date)]
    public DateTime? DataWhenPayed { get; init; }
}
