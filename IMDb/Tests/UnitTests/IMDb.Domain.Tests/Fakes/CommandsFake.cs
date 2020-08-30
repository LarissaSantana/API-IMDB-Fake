using IMDb.Domain.Commands.Movie;
using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace IMDb.Domain.Tests.Fakes
{
    public class CommandsFake
    {
        public static AddMovieCommand GenerateAddMovieCommandFake(
            string title = null,
            Genre genre = Genre.SciFi,
            List<Guid> castIds = null)
        {
            return new AddMovieCommand
                (
                    title: title,
                    genre: genre,
                    castIds: castIds
                );
        }
    }
}
