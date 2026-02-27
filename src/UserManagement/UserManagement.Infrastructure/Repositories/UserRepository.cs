using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _db;
    public UserRepository(UserDbContext db) => _db = db;

    public async Task<List<User>> GetAllAsync(CancellationToken ct = default)
        => await _db.Users.OrderByDescending(u => u.CreatedUtc).ToListAsync(ct);

    public async Task<User> AddAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return user;
    }
}
