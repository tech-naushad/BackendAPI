using APICore.DomainEvents;
using MediatR;
using MessageBrokers.NotificationPublisher;


namespace IdentityManagement.Application.Commands.CreateUser
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly INotificationService _notificationService;
        public UserCreatedEventHandler(INotificationService notificationService)
       // public UserCreatedEventHandler()
        {
            _notificationService = notificationService;
        }
        public async Task Handle(UserCreatedEvent notificationEvent, CancellationToken cancellationToken)
        {
            //var arg = new EmailEventArg {  From = "test@example.com", To = notificationEvent.Email, 
               // Body = $"New User {notificationEvent.Name} created with email {notificationEvent.Email}", Subject = $"User {notificationEvent.Name} created" };
          //  await _notificationService.SubscribeEmailNotificationAsync(arg);
           // await Task.FromResult(notification);
        }
    }
}
