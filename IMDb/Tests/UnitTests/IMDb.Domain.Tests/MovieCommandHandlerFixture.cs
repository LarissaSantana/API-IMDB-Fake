using IMDb.Domain.Commands.Movie;
using IMDb.Domain.Core.Data;
using IMDb.Domain.Entities;
using IMDb.Domain.Repositories;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace IMDb.Domain.Tests
{
    [CollectionDefinition(nameof(MovieCommandHandlerCollection))]
    public class MovieCommandHandlerCollection : ICollectionFixture<MovieCommandHandlerFixture> { }
    public class MovieCommandHandlerFixture
    {
        public MovieCommandHandler MovieCommandHandler;
        public AutoMocker Mocker;

        public MovieCommandHandler GetMovieCommandHandler()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<MovieCommandHandler>();
        }

        public void GetCastSetup(List<Cast> casts)
        {
            Mocker.GetMock<IMovieRepository>()
                .Setup(x => x.GetCast(It.IsAny<Expression<Func<Cast, bool>>>()))
                .Returns(casts);
        }

        public void CommitSetup(bool success = true)
        {
            Mocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit())
                .Returns(success);
        }
    }
}
