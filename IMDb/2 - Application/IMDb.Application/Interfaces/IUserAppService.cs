using IMDb.Application.ViewModels;
using IMDb.Application.ViewModels.Add;
using System;

namespace IMDb.Application.Interfaces
{
    public interface IUserAppService
    {
        void AddUser(AddUserViewModel viewModel);
        void UpdateUser(UpdateUserViewModel viewModel);
        void ChangeStatus(Guid userId, bool status);
    }
}
