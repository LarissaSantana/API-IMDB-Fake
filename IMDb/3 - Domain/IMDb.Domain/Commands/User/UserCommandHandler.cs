using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Messages;
using IMDb.Domain.Core.Notifications;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Domain.Commands.User
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<AddUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public UserCommandHandler(IUserRepository userRepository, IUnitOfWork uow, IMediatorHandler bus,
           IDomainNotificationHandler<DomainNotification> notifications)
           : base(bus, uow, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddUserCommand message, CancellationToken cancellationToken)
        {
            var roleExists = _userRepository.GetRole(it => it.Id == message.RoleId).Any();
            if (!roleExists) NotifyValidationErrors("Role not found.");

            //TODO: implementar criptografia para a senha
            var user = UserFactory.Create(message.Name, message.RoleId, message.Password);

            if (!user.IsValid()) NotifyValidationErrors(user.ValidationResult);

            _userRepository.Add(user);
            return await Task.FromResult(Commit());
        }
    }
}
