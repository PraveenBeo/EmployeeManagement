using Microsoft.EntityFrameworkCore;
using LeaveManagement.Domain.Entities;

namespace LeaveManagement.Infrastructure.Data;

public class LeaveDbContext : DbContext
{
    public LeaveDbContext(DbContextOptions<LeaveDbContext> options) : base(options) { }
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
}
