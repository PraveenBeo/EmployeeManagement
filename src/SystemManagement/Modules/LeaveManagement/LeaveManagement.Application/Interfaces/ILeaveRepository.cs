using LeaveManagement.Domain.Entities;

namespace LeaveManagement.Application.Interfaces;

public interface ILeaveRepository
{
    Task<List<LeaveRequest>> GetAllAsync(CancellationToken ct = default);
    Task<LeaveRequest> AddAsync(LeaveRequest leave, CancellationToken ct = default);
}
