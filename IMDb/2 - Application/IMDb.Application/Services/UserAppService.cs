using AutoMapper;
using IMDb.Application.Interfaces;
using IMDb.Application.ViewModels;
using IMDb.Application.ViewModels.Add;
using IMDb.Domain.Commands.User;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Repositories;

namespace IMDb.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        public UserAppService(IUserRepository userRepository, IMapper mapper, IMediatorHandler bus)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public void AddUser(AddUserViewModel viewModel)
        {
            var map = _mapper.Map<AddUserCommand>(viewModel);
            _bus.SendCommand(map);
        }

        public void UpdateUser(UpdateUserViewModel viewModel)
        {
            var map = _mapper.Map<UpdateUserCommand>(viewModel);
            _bus.SendCommand(map);
        }
    }
}
