using AutoMapper;
using IMDb.Application.Interfaces;
using IMDb.Application.ViewModels.Add;
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
            throw new System.NotImplementedException();
        }
    }
}
