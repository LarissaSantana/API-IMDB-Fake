using AutoMapper;
using IMDb.Application.ViewModels;
using IMDb.Application.ViewModels.Add;
using IMDb.Domain.Commands;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Repositories;
using System;

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
    }
}
