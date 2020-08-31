using AutoMapper;
using IMDb.Application.Services.Interfaces;
using IMDb.Application.ViewModels.Movie;
using IMDb.Application.ViewModels.Movie.Add;
using IMDb.Application.ViewModels.Movie.Return;
using IMDb.Domain.Commands.Movie;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using IMDb.Domain.Utility;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IMDb.Application.Services
{
    public class MovieAppService : IMovieAppService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        public MovieAppService(IMovieRepository movieRepository, IMapper mapper, IMediatorHandler bus)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public void AddMovie(AddMovieViewModel viewModel)
        {
            var map = _mapper.Map<AddMovieCommand>(viewModel);
            _bus.SendCommand(map);
        }

        public void AddRatingOfMovie(AddRatingOfMovieViewModel viewModel)
        {
            var map = _mapper.Map<AddRatingOfMovieCommand>(viewModel);
            _bus.SendCommand(map);
        }

        public void AddCast(AddCastViewModel viewModel)
        {
            var map = _mapper.Map<AddCastCommand>(viewModel);
            _bus.SendCommand(map);
        }

        public MovieViewModel GetMovieById(Guid id)
        {
            var movie = _movieRepository.GetMovieById(id);
            return _mapper.Map<MovieViewModel>(movie);
        }

        public Pagination<MovieWithRatingViewModel> GetMovies(MovieFilterViewModel viewModel, int pageNumber, int pageSize)
        {
            Expression<Func<Movie, bool>> predicate = ExpressionExtension.Query<Movie>();

            if (viewModel == null)
            {
                if (viewModel.Genre.HasValue)
                    predicate = predicate.And(it => it.Genre == viewModel.Genre);

                if (!string.IsNullOrWhiteSpace(viewModel.Title))
                    predicate = predicate.And(it => it.Title.ToLower().Equals(viewModel.Title.ToLower()));

                if (viewModel.CastIds != null && viewModel.CastIds.Any())
                    predicate = predicate.And(it => it.CastOfMovies.Any(x => viewModel.CastIds.Contains(x.Cast.Id)));
            }

            var moviesPagination = _movieRepository.GetMoviesWithPagination(predicate, pageNumber, pageSize);
            var map = _mapper.Map<Pagination<MovieWithRatingViewModel>>(moviesPagination);
            return map;
        }
    }
}
