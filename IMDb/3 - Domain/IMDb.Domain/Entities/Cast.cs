using IMDb.Domain.DomainObjects;
using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace IMDb.Domain.Entities
{
    public class Cast : BaseEntity<Cast>
    {
        public string Name { get; private set; }
        public CastType CastType { get; private set; }
        public virtual ICollection<CastOfMovie> CastOfMovies { get; private set; }

        protected Cast()
        {
            CastOfMovies = new List<CastOfMovie>();
        }

        public Cast(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public static class CastFactory
        {
            public static Cast Create(string name, CastType castType)
            {
                var cast = new Cast
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    CastType = castType
                };

                return cast;
            }
        }
    }
}
