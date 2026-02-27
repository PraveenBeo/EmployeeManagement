namespace BuildingBlocks.Messaging.IntegrationEvents;

public record UserCreatedIntegrationEvent
{
    public Guid UserId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime OccurredUtc { get; init; } = DateTime.UtcNow;
}
