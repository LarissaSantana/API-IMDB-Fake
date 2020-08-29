using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Commands
{
    public class AddMeanCommand : Command
    {
        public Guid MovieId { get; set; }

        public AddMeanCommand(Guid movieId)
        {
            MovieId = movieId;
        }
    }
}
