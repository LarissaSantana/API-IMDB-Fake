using IMDb.Domain.Core.Messages;
using System;

namespace IMDb.Domain.Events
{
    public class RatingOfMovieAddedEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public int Rate { get; private set; }
        public Guid MovieId { get; private set; }

        public RatingOfMovieAddedEvent(Guid id, int rate, Guid movieId) : base(id)
        {
            Rate = rate;
            MovieId = movieId;
        }
    }
}
