using BuildingBlocks.Abstractions.Common;

namespace AccessCardManagement.Domain.Entities;

public class AccessCard : EntityBase
{
    public string CardNumber { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
}
