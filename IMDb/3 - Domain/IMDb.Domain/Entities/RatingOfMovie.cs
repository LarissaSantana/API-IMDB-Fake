using IMDb.Domain.DomainObjects;
using System;

namespace IMDb.Domain.Entities
{
    public class RatingOfMovie : BaseEntity<RatingOfMovie>
    {
        public int Rate { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }
        public Guid MovieId { get; private set; }
        public virtual Movie Movie { get; private set; }

        protected RatingOfMovie() { }

        public RatingOfMovie(Guid id, Guid userId, Guid movieId)
        {
            Id = id;
            UserId = userId;
            MovieId = movieId;
        }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
