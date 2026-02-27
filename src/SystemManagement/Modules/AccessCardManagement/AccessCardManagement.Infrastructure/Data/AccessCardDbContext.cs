using Microsoft.EntityFrameworkCore;
using AccessCardManagement.Domain.Entities;

namespace AccessCardManagement.Infrastructure.Data;

public class AccessCardDbContext : DbContext
{
    public AccessCardDbContext(DbContextOptions<AccessCardDbContext> options) : base(options) { }
    public DbSet<AccessCard> AccessCards => Set<AccessCard>();
}
