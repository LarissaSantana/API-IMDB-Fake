using IMDb.Domain.Core.Bus;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Core.Messages;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IMDb.Domain.Commands
{
    public class MovieCommandHandler : CommandHandler,
        IRequestHandler<AddMovieCommand, bool>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMediatorHandler _bus;

        public MovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork uow, IMediatorHandler bus)
            : base(bus, uow)
        {
            _movieRepository = movieRepository;
            _bus = bus;
        }

        public async Task<bool> Handle(AddMovieCommand message, CancellationToken cancellationToken)
        {
            //cast        
            var movie = Movie.MovieFactory.Create(message.Genre, message.Title);
            var castOfMovieList = new List<CastOfMovie>();

            foreach (var cast in message.Cast)
            {
                var castModel = Cast.CastFactory.Create(cast.Name, cast.CastType);
                if (!castModel.IsValid())
                {
                    //TODO: notificar erro da validação da entidade
                }

                var castOfMovieModel = CastOfMovie.CastOfMovieFactory.Create(movie.Id, castModel.Id);
                castOfMovieModel.AddCast(castModel);
                if (!castOfMovieModel.IsValid())
                {
                    //TODO: notificar erro da validação da entidade
                }

                castOfMovieList.Add(castOfMovieModel);
            }

            movie.AddCastOfMovie(castOfMovieList);
            if (!movie.IsValid())
            {
                //TODO: notificar erro da validação da entidade
            }

            _movieRepository.Add(movie);

            //TODO: implementar o Commit() no CommandHandler
            if (_uow.Commit()) return await Task.FromResult(true);

            return false;
        }

        //public async Task<bool> Handle(AddCastCommand message, CancellationToken cancellationToken)
        //{

        //    var cast = Cast.CastFactory.Create(message.Name, message.CastType);

        //    _movieRepository.AddCast(cast);

        //    return await Task.FromResult(_uow.Commit());
        //}
    }
}
