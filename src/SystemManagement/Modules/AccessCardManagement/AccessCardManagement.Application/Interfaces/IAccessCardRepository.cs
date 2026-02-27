using AccessCardManagement.Domain.Entities;

namespace AccessCardManagement.Application.Interfaces;

public interface IAccessCardRepository
{
    Task<List<AccessCard>> GetAllAsync(CancellationToken ct = default);
    Task<AccessCard> AddAsync(AccessCard card, CancellationToken ct = default);
}
