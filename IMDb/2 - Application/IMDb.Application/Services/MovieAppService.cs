﻿using AutoMapper;
using IMDb.Application.ViewModels.Add;
using IMDb.Application.ViewModels.Filters;
using IMDb.Application.ViewModels.Return;
using IMDb.Domain.Commands;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Extension;
using IMDb.Domain.Core.Pagination;
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

        public Pagination<MovieWithRatingViewModel> GetMoviesWithPagination(MovieFilterViewModel viewModel, int pageNumber, int pageSize)
        {
            Expression<Func<Movie, bool>> predicate = ExpressionExtension.Query<Movie>();

            if (viewModel.Genre.HasValue)
                predicate = predicate.And(it => it.Genre == viewModel.Genre);

            if (!string.IsNullOrWhiteSpace(viewModel.Title))
                predicate = predicate.And(it => it.Title.ToLower().Equals(viewModel.Title.ToLower()));

            if (viewModel.CastIds != null && viewModel.CastIds.Any())
                predicate = predicate.And(it => it.CastOfMovies.Any(x => viewModel.CastIds.Contains(x.Cast.Id)));

            var moviePagination = _movieRepository.GetMoviesWithPagination(predicate, pageNumber, pageSize);
            var map = _mapper.Map<Pagination<MovieWithRatingViewModel>>(moviePagination);
            return map;
        }
    }
}
