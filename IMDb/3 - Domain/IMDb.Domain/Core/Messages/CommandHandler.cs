using FluentValidation.Results;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Notifications;
using System.Collections.Generic;
using System.Threading;

namespace IMDb.Domain.Core.Messages
{
    public abstract class CommandHandler
    {
        protected readonly IMediatorHandler _bus;
        protected readonly IUnitOfWork _uow;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public CommandHandler(IMediatorHandler bus, IUnitOfWork uow,
            IDomainNotificationHandler<DomainNotification> notifications)
        {
            _bus = bus;
            _uow = uow;
            _notifications = notifications;
        }

        protected void NotifyValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Handle(new DomainNotification(error.PropertyName, error.ErrorMessage),
                    CancellationToken.None);
            }
        }

        protected void NotifyValidationErrors(string errorMessage)
        {
            var errors = new List<ValidationFailure> { new ValidationFailure(null, errorMessage) };

            var validationResult = new ValidationResult(errors);

            NotifyValidationErrors(validationResult);
        }

        protected bool HasNotifications()
        {
            return _notifications.HasNotifications();
        }

        public bool Commit()
        {
            if (HasNotifications()) return false;
            if (_uow.Commit()) return true;

            NotifyValidationErrors("Commit - We had a problem during saving your data.");
            return false;
        }
    }
}
