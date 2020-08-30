using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace IMDb.Application.ViewModels.Movie.Return
{
    public abstract class BaseMovieViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public float? Mean { get; set; }
        public ICollection<CastOfMovieViewModel> CastOfMovie { get; set; }
    }
}
