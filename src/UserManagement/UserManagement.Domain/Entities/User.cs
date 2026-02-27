using BuildingBlocks.Abstractions.Common;

namespace UserManagement.Domain.Entities;

public class User : EntityBase
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
