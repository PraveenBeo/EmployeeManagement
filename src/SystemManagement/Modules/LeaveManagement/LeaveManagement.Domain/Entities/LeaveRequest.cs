using BuildingBlocks.Abstractions.Common;

namespace LeaveManagement.Domain.Entities;

public class LeaveRequest : EntityBase
{
    public string EmployeeName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}
