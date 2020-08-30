using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands.Movie
{
    public class AddMeanCommand : Command
    {
        public Guid MovieId { get; private set; }

        public AddMeanCommand(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
