using AutoMapper;
using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Filters;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Commands;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Extension;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using System;
using System.Collections.Generic;
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
            //TODO: inserir por usuário logado
            var map = _mapper.Map<AddRatingOfMovieCommand>(viewModel);
            _bus.SendCommand(map);
        }

        public MovieViewModel GetMovieById(Guid id)
        {
            var movie = _movieRepository.GetMovieById(id);
            return _mapper.Map<MovieViewModel>(movie);
        }

        public IEnumerable<MovieWithRatingViewModel> GetMovies(MovieFilterViewModel viewModel)
        {
            Expression<Func<Movie, bool>> predicate = ExpressionExtension.Query<Movie>();

            if (viewModel.Genre.HasValue)
                predicate = predicate.And(it => it.Genre == viewModel.Genre);

            if (!string.IsNullOrWhiteSpace(viewModel.Title))
                predicate = predicate.And(it => it.Title.ToLower().Equals(viewModel.Title.ToLower()));

            if (viewModel.CastList != null && viewModel.CastList.Any())
            {
                foreach (var cast in viewModel.CastList)
                {
                    predicate = predicate.And(it => it.CastOfMovies
                        .Any(x => x.Cast.Name.Equals(cast.Name) && x.Cast.CastType == cast.CastType));
                }
            }

            var movies = _movieRepository.GetMovies(predicate);
            return _mapper.Map<IEnumerable<MovieWithRatingViewModel>>(movies);
        }
    }
}
