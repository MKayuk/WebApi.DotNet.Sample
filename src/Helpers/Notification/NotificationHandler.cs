using MediatR;

namespace WebApi.DotNet.Sample.Helpers.Notification
{
    public class NotifiyHandler : INotificationHandler<Notification>
    {
        private readonly IList<Notification> _notifications;

        public NotifiyHandler()
        {
            _notifications = new List<Notification>();
        }

        public Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        public virtual IEnumerable<Notification> GetNotifications()
        {
            return _notifications.Where(n => n.GetType() == typeof(Notification));
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
