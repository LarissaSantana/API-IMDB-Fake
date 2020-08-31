using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Messages;
using IMDb.Domain.Core.Notifications;
using IMDb.Domain.Core.Security;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Domain.Commands.User
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<AddUserCommand, bool>,
        IRequestHandler<UpdateUserCommand, bool>,
        IRequestHandler<ChangeStatusCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurity _security;
        public UserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            IDomainNotificationHandler<DomainNotification> notifications,
            ISecurity security)
           : base(bus, uow, notifications)
        {
            _userRepository = userRepository;
            _security = security;
        }

        public async Task<bool> Handle(AddUserCommand message, CancellationToken cancellationToken)
        {
            var roleExists = _userRepository.GetRole(it => it.Id == message.RoleId).Any();
            if (!roleExists)
            {
                NotifyValidationErrors("Role not found.");
                return false;
            }

            var hashPassword = _security.Encrypt(message.Password, message.Name);
            var user = UserFactory.Create(message.Name, message.RoleId, hashPassword);

            if (!user.IsValid()) NotifyValidationErrors(user.ValidationResult);

            _userRepository.Add(user);
            return await Task.FromResult(Commit());
        }

        public async Task<bool> Handle(UpdateUserCommand message, CancellationToken cancellationToken)
        {
            if (!message.Id.Equals(_userRepository.GetUserAuthenticatedId()))
            {
                NotifyValidationErrors("Permission denied to change this user.");
                return false;
            }

            var user = _userRepository.GetById(message.Id);
            if (user == null)
            {
                NotifyValidationErrors("User not found.");
                return false;
            }

            user.ChangeName(message.Name);

            if (!user.IsValid()) NotifyValidationErrors(user.ValidationResult);

            _userRepository.Update(user);

            return await Task.FromResult(Commit());
        }

        public async Task<bool> Handle(ChangeStatusCommand message, CancellationToken cancellationToken)
        {
            if (!message.Id.Equals(_userRepository.GetUserAuthenticatedId()))
            {
                NotifyValidationErrors("Permission denied to change this user.");
                return false;
            }

            var user = _userRepository.GetById(message.Id);
            if (user == null)
            {
                NotifyValidationErrors("User not found.");
                return false;
            }

            user.ChangeStatus(message.Status);

            if (!user.IsValid()) NotifyValidationErrors(user.ValidationResult);

            _userRepository.Update(user);

            return await Task.FromResult(Commit());
        }
    }
}
