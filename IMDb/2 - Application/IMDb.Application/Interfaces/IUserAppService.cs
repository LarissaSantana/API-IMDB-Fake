using IMDb.Application.ViewModels;
using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Entities;
using IMDb.Domain.Utility;
using System;

namespace IMDb.Application.Interfaces
{
    public interface IUserAppService
    {
        void AddUser(AddUserViewModel viewModel);
        void UpdateUser(UpdateUserViewModel viewModel);
        void ChangeStatus(Guid userId, bool status);
        Pagination<UserViewModel> GetNonActiveteCommonUsers(int pageNumber, int pageSize);

        User GetUsersByNameAndPassword(string name, string password);
    }
}
