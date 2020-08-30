using IMDb.Application.ViewModels;
using IMDb.Application.ViewModels.Add;

namespace IMDb.Application.Interfaces
{
    public interface IUserAppService
    {
        void AddUser(AddUserViewModel viewModel);
        void UpdateUser(UpdateUserViewModel viewModel);
    }
}
