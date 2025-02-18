using APIContract.DomainEvents;
using MediatR;

namespace Infrastructure.DomainEventHandlers
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.FromResult(notification);
        }
    }
}
