using MediatR;

namespace WebApi.DotNet.Sample.Helpers.Notification
{
    public class Notify : INotify
    {
        private readonly NotifiyHandler _messageHandler;

        public Notify(INotificationHandler<Notification> notification)
        {
            _messageHandler = (NotifiyHandler)notification;
        }

        public Notify Invoke()
        {
            return this;
        }

        public bool IsValid()
        {
            return !_messageHandler.HasNotifications();
        }

        public void Add(string key, string message)
        {
            _messageHandler.Handle(new Notification(key, message), default(CancellationToken));
        }
    }
}
