using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(CancellationToken ct = default);
    Task<User> AddAsync(User user, CancellationToken ct = default);
}
