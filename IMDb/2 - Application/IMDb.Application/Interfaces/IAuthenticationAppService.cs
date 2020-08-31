using IMDb.Application.ViewModels.User;

namespace IMDb.Application.Interfaces
{
    public interface IAuthenticationAppService
    {
        string Authenticate(UserLoginViewModel viewModel, out string error);
    }
}
