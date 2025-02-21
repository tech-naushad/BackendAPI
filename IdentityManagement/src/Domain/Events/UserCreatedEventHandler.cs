using APICore.DomainEvents;
using MediatR;

namespace Domain.Events
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.FromResult(notification);
        }
    }
}
