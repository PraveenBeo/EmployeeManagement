using Microsoft.EntityFrameworkCore;
using AccessCardManagement.Application.Interfaces;
using AccessCardManagement.Domain.Entities;
using AccessCardManagement.Infrastructure.Data;

namespace AccessCardManagement.Infrastructure.Repositories;

public class AccessCardRepository : IAccessCardRepository
{
    private readonly AccessCardDbContext _db;
    public AccessCardRepository(AccessCardDbContext db) => _db = db;

    public async Task<List<AccessCard>> GetAllAsync(CancellationToken ct = default)
        => await _db.AccessCards.OrderByDescending(c => c.CreatedUtc).ToListAsync(ct);

    public async Task<AccessCard> AddAsync(AccessCard card, CancellationToken ct = default)
    {
        _db.AccessCards.Add(card);
        await _db.SaveChangesAsync(ct);
        return card;
    }
}
