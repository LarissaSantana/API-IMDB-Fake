using AutoMapper;
using IMDb.Application.ViewModels.Add;
using IMDb.Domain.Commands;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Repositories;

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
    }
}
