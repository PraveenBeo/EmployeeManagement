using BuildingBlocks.Abstractions.Common;

namespace ReportManagement.Domain.Entities;

public class Report : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
