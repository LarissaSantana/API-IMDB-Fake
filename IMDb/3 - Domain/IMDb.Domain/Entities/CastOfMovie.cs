using IMDb.Domain.DomainObjects;
using System;

namespace IMDb.Domain.Entities
{
    public class CastOfMovie : BaseEntity<CastOfMovie>
    {
        public Guid MovieId { get; private set; }
        public virtual Movie Movie { get; private set; }
        public Guid CastId { get; private set; }
        public virtual Cast Cast { get; private set; }

        protected CastOfMovie() { }

        public CastOfMovie(Guid id, Guid movieId, Guid castId)
        {
            Id = id;
            MovieId = id;
            CastId = id;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
