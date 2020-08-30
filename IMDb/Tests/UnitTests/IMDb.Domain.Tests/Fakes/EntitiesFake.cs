using IMDb.Domain.Entities;
using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;
using static IMDb.Domain.Entities.Cast;

namespace IMDb.Domain.Tests.Fakes
{
    public class EntitiesFake
    {
        public static Cast GenerateCastFake(
            Guid? id = null,
            string name = null,
            CastType type = CastType.Director,
            List<CastOfMovie> castOfMovies = null)
        {
            var cast = CastFactory.Create
                (
                    id: id,
                    name: name ?? "Denis Villeneuve",
                    castType: type
                );

            cast.AddCastOfMovies(castOfMovies);

            return cast;
        }
    }
}
