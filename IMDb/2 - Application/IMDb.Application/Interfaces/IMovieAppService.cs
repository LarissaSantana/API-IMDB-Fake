using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Filters;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Utility;
using System;

namespace IMDb.Application.Services.Interfaces
{
    public interface IMovieAppService
    {
        void AddMovie(AddMovieViewModel viewModel);
        void AddRatingOfMovie(AddRatingOfMovieViewModel viewModel);
        void AddCast(AddCastViewModel viewModel);
        MovieViewModel GetMovieById(Guid id);
        Pagination<MovieWithRatingViewModel> GetMovies(MovieFilterViewModel viewModel, int pageNumber, int pageSize);
    }
}
