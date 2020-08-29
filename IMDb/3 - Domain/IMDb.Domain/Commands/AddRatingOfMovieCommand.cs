using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands
{
    public class AddRatingOfMovieCommand : Command
    {
        public int Rate { get; private set; }
        public Guid MovieId { get; private set; }
        public Guid UserId { get; private set; }

        public AddRatingOfMovieCommand(int rate, Guid movieId, Guid userId)
        {
            Rate = rate;
            MovieId = movieId;
            UserId = userId;
        }
    }
}
