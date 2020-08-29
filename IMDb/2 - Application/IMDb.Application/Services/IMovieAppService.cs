using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Filters;
using IMDb.Application.ViewModels.Return;
using System;
using System.Collections.Generic;

namespace IMDb.Application.Services
{
    public interface IMovieAppService
    {
        void AddMovie(AddMovieViewModel viewModel);
        void AddRatingOfMovie(AddRatingOfMovieViewModel viewModel);
        MovieViewModel GetMovieById(Guid id);
        IEnumerable<MovieWithRatingViewModel> GetMovies(MovieFilterViewModel viewModel);
    }
}
