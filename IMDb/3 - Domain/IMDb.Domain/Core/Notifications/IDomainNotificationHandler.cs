using IMDb.Domain.Core.Messages;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : INotificationHandler<T> where T : Event
    {
        bool HasNotifications();
        List<T> GetNotifications();
        Task Handle(DomainNotification message, CancellationToken cancellationToken);
    }
}
