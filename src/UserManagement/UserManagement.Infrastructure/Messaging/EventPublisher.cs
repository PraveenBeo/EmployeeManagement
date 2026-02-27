using MassTransit;
using BuildingBlocks.Messaging.IntegrationEvents;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Messaging;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventPublisher(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task PublishUserCreatedAsync(User user, CancellationToken ct = default)
    {
        var integrationEvent = new UserCreatedIntegrationEvent
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            OccurredUtc = DateTime.UtcNow
        };

        await _publishEndpoint.Publish(integrationEvent, ct);
    }
}
