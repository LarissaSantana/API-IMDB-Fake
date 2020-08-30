using IMDb.Domain.Commands.Movie;
using IMDb.Domain.Core.Notifications;
using IMDb.Domain.Entities;
using IMDb.Domain.Enums;
using IMDb.Domain.Repositories;
using IMDb.Domain.Tests.Fakes;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Xunit;

namespace IMDb.Domain.Tests
{
    [Collection(nameof(MovieCommandHandlerCollection))]
    public class MovieCommandHandlerTests
    {
        private readonly MovieCommandHandler _commandHandler;
        private readonly MovieCommandHandlerFixture _fixture;

        public MovieCommandHandlerTests(MovieCommandHandlerFixture fixture)
        {
            _fixture = fixture;
            _commandHandler = _fixture.GetMovieCommandHandler();
        }

        [Fact(DisplayName = "Add movie with success")]
        [Trait("Movie", "MovieCommandHandler Tests")]
        public void AddMovieCommand_MustRunSuccessfully()
        {
            //Arrange
            var actor1 = EntitiesFake.GenerateCastFake(id: Guid.NewGuid());
            var actor2 = EntitiesFake.GenerateCastFake(id: Guid.NewGuid());
            var director = EntitiesFake.GenerateCastFake(id: Guid.NewGuid());
            var casts = new List<Cast>() { actor1, actor2, director };
            _fixture.GetCastSetup(casts);

            var castIds = new List<Guid>() { actor1.Id, actor2.Id, director.Id };
            var addMovieCommand = CommandsFake.GenerateAddMovieCommandFake(
                title: "Arrival",
                genre: Genre.SciFi,
                castIds: castIds);

            _fixture.CommitSetup();

            //Act
            var result = _commandHandler.Handle(addMovieCommand, CancellationToken.None);

            //Assert
            _fixture.Mocker.GetMock<IMovieRepository>()
                .Verify(x => x.GetCast(It.IsAny<Expression<Func<Cast, bool>>>()), Times.Once);

            _fixture.Mocker.GetMock<IMovieRepository>()
                .Verify(x => x.Add(It.Is<Movie>(movie =>
                movie.Title == addMovieCommand.Title &&
                movie.Genre == addMovieCommand.Genre &&
                addMovieCommand.CastIds.All(x => movie.CastOfMovies.Select(x => x.CastId).Contains(x)))),
                Times.Once);

            _fixture.Mocker.GetMock<INotificationHandler<DomainNotification>>()
                .Verify(x => x.Handle(It.IsAny<DomainNotification>(), It.IsAny<CancellationToken>()),
                Times.Never);

            Assert.True(result.Result);
        }

        //TODO: TESTE ADD MOVIE - QUANDO NÃO HÁ CAST NO REPOSITÓRIO DEVE RETORNAR MENSAGEM DE ERRO
        //TODO: TESTE ADD MOVIE - NA LISTA DE CAST DEVE HAVER PELO MENOS UM DIRETOR
        //TODO: TESTE ADD MOVIE - INLINE_DATA PARA VALIDAR QUE TEM PELO MENOS UM ATOR QUANDO O GENERO NÃO É ANIMATION
        //TODO: TESTE ADD MOVIE - CASO RETORNE ERRO DE VALIDAÇÃO DO DOMÍNIO PARA CASTOFMOVIE
        //TODO: TESTE ADD MOVIE - CASO RETORNE ERRO DE VALIDAÇÃO DO DOMÍNIO PARA MOVIE

        //TODO: TESTE ADD CAST - COM SUCESSO
        //TODO: TESTE ADD CAST - CASO RETORNE VALIDAÇÃO DE ERRO DO DOMÍNIO

        //TODO: TESTE ADD RATING OF MOVIE - COM SUCESSO
        //TODO: TESTE ADD RATING OF MOVIE - QUANDO O USUARIO LOGADO É NULL DEVE RETORNAR ERRO
        //TODO: TESTE ADD RATING OF MOVIE - QUANDO REGISTRO DE NOTA NÃO EXISTE NO BANCO COM SUCESSO
        //TODO: TESTE ADD RATING OF MOVIE - QUANDO REGISTRO DE NOTA NÃO EXISTE NO BANCO COM FALHA DE DOMINIO
        //TODO: TESTE ADD RATING OF MOVIE - QUANDO REGISTRO DE NOTA EXISTE NO BANCO COM SUCESSO
        //TODO: TESTE ADD RATING OF MOVIE - QUANDO REGISTRO DE NOTA EXISTE NO BANCO COM FALHA DE DOMINIO

        //TODO: TESTE ADD MEAN - COM SUCESSO
        //TODO: TESTE ADD MEAN - QUANDO NÃO HÁ REGISTRO NO BANCO
        //TODO: TESTE ADD MEAN - QUANDO MOVIE NÃO ESTÁ NO INCLUDE RETURN FALSE
    }
}
