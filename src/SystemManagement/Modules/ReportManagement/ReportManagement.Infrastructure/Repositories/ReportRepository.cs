using Microsoft.EntityFrameworkCore;
using ReportManagement.Application.Interfaces;
using ReportManagement.Domain.Entities;
using ReportManagement.Infrastructure.Data;

namespace ReportManagement.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly ReportDbContext _db;
    public ReportRepository(ReportDbContext db) => _db = db;

    public async Task<List<Report>> GetAllAsync(CancellationToken ct = default)
        => await _db.Reports.OrderByDescending(r => r.CreatedUtc).ToListAsync(ct);

    public async Task<Report> AddAsync(Report report, CancellationToken ct = default)
    {
        _db.Reports.Add(report);
        await _db.SaveChangesAsync(ct);
        return report;
    }
}
