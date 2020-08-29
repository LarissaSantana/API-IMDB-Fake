using IMDb.Application.ViewModels;
using IMDb.Application.ViewModels.Add;
using System;

namespace IMDb.Application.Services
{
    public interface IMovieAppService
    {
        void AddMovie(AddMovieViewModel viewModel);
        void AddRatingOfMovie(AddRatingOfMovieViewModel viewModel);
        MovieViewModel GetMovieById(Guid id);
    }
}
