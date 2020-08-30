using AutoMapper;
using IMDb.Application.Interfaces;
using IMDb.Application.ViewModels;
using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Commands.User;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using IMDb.Domain.Utility;
using System;
using System.Linq.Expressions;

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

        public void ChangeStatus(Guid userId, bool status)
        {
            var command = new ChangeStatusCommand(userId, status);
            _bus.SendCommand(command);
        }

        public void UpdateUser(UpdateUserViewModel viewModel)
        {
            var map = _mapper.Map<UpdateUserCommand>(viewModel);
            _bus.SendCommand(map);
        }

        public Pagination<UserViewModel> GetNonActiveteCommonUsers(int pageNumber, int pageSize)
        {
            Expression<Func<User, bool>> predicate = ExpressionExtension.Query<User>();            
            predicate = predicate.And(user => user.RoleId == RoleIdentify.Common.Value)
                                 .And(user => user.Status);

            var usersPagination = _userRepository.GetUsersWithPagination(predicate, pageNumber, pageSize);
            return _mapper.Map<Pagination<User>, Pagination<UserViewModel>>(usersPagination);
        }
    }
}
