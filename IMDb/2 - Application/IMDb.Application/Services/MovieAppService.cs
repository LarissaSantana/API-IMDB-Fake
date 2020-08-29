using AutoMapper;
using IMDb.Application.ViewModels.Add;
using IMDb.Domain.Core.Bus;
using IMDb.Domain.Repositories;

namespace IMDb.Application.Services
{
    public class MovieAppService : IMovieAppService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        public void AddMovie(AddMovieViewModel viewModel)
        {

        }
    }
}
