using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IEventPublisher
{
    Task PublishUserCreatedAsync(User user, CancellationToken ct = default);
}
