using Microsoft.EntityFrameworkCore;
using LeaveManagement.Application.Interfaces;
using LeaveManagement.Domain.Entities;
using LeaveManagement.Infrastructure.Data;

namespace LeaveManagement.Infrastructure.Repositories;

public class LeaveRepository : ILeaveRepository
{
    private readonly LeaveDbContext _db;
    public LeaveRepository(LeaveDbContext db) => _db = db;

    public async Task<List<LeaveRequest>> GetAllAsync(CancellationToken ct = default)
        => await _db.LeaveRequests.OrderByDescending(l => l.CreatedUtc).ToListAsync(ct);

    public async Task<LeaveRequest> AddAsync(LeaveRequest leave, CancellationToken ct = default)
    {
        _db.LeaveRequests.Add(leave);
        await _db.SaveChangesAsync(ct);
        return leave;
    }
}
