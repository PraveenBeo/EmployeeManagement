using MassTransit;
using BuildingBlocks.Messaging.IntegrationEvents;
using AccessCardManagement.Application.Interfaces;
using AccessCardManagement.Domain.Entities;

namespace AccessCardManagement.Infrastructure.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreatedIntegrationEvent>
{
    private readonly IAccessCardRepository _accessCardRepository;

    public UserCreatedConsumer(IAccessCardRepository accessCardRepository)
        => _accessCardRepository = accessCardRepository;

    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        var accessCard = new AccessCard
        {
            CardNumber = $"AC-{message.UserId.ToString()[..8].ToUpper()}",
            EmployeeName = message.FullName
        };

        await _accessCardRepository.AddAsync(accessCard);
    }
}
