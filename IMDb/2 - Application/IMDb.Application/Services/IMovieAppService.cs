using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Filters;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Core.Pagination;
using System;

namespace IMDb.Application.Services
{
    public interface IMovieAppService
    {
        void AddMovie(AddMovieViewModel viewModel);
        void AddRatingOfMovie(AddRatingOfMovieViewModel viewModel);
        MovieViewModel GetMovieById(Guid id);
        Pagination<MovieWithRatingViewModel> GetMoviesWithPagination(MovieFilterViewModel viewModel, int pageNumber, int pageSize);
    }
}
