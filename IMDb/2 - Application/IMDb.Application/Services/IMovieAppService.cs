using IMDb.Application.ViewModels.Add;

namespace IMDb.Application.Services
{
    public interface IMovieAppService
    {
        void AddMovie(AddMovieViewModel viewModel);
    }
}
