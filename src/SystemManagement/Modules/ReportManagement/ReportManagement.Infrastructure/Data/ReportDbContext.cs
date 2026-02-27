using Microsoft.EntityFrameworkCore;
using ReportManagement.Domain.Entities;

namespace ReportManagement.Infrastructure.Data;

public class ReportDbContext : DbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options) { }
    public DbSet<Report> Reports => Set<Report>();
}
