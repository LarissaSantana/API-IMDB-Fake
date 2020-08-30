using IMDb.Domain.Commands.Movie;
using Moq.AutoMock;
using Xunit;

namespace IMDb.Domain.Tests
{
    [CollectionDefinition(nameof(MovieCommandHandlerCollection))]
    public class MovieCommandHandlerCollection : ICollectionFixture<MovieCommandHandlerTestsFixture> { }
    public class MovieCommandHandlerTestsFixture
    {
        public MovieCommandHandler MovieCommandHandler;
        public AutoMocker Mocker;

        public MovieCommandHandler GetMovieCommandHandler()
        {
            Mocker = new AutoMocker();
            return Mocker.CreateInstance<MovieCommandHandler>();
        }
    }
}
