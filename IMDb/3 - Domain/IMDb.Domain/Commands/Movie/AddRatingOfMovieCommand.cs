using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands.Movie
{
    public class AddRatingOfMovieCommand : Command
    {
        public int Rate { get; private set; }
        public Guid MovieId { get; private set; }

        public AddRatingOfMovieCommand(int rate, Guid movieId)
        {
            Rate = rate;
            MovieId = movieId;
        }
    }
}
