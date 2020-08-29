using IMDb.Domain.Core.Messages;
using IMDb.Domain.Enums;
using System.Collections.Generic;

namespace IMDb.Domain.Commands
{
    public class AddMovieCommand : Command
    {
        public string Title { get; private set; }
        public Genre Genre { get; private set; }
        public List<AddCastCommand> Cast { get; private set; }

        public AddMovieCommand(string title, Genre genre, List<AddCastCommand> cast)
        {
            Title = title;
            Genre = genre;
            Cast = cast;
        }
    }
}
