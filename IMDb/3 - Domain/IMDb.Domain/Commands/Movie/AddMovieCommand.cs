using IMDb.Domain.Core.Messages;
using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace IMDb.Domain.Commands.Movie
{
    public class AddMovieCommand : Command
    {
        public string Title { get; private set; }
        public Genre Genre { get; private set; }
        public List<Guid> CastIds { get; private set; }

        public AddMovieCommand(string title, Genre genre, List<Guid> castIds)
        {
            Title = title;
            Genre = genre;
            CastIds = castIds;
        }
    }
}
