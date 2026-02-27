using ReportManagement.Domain.Entities;

namespace ReportManagement.Application.Interfaces;

public interface IReportRepository
{
    Task<List<Report>> GetAllAsync(CancellationToken ct = default);
    Task<Report> AddAsync(Report report, CancellationToken ct = default);
}
