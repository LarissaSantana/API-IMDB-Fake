using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace IMDb.Application.ViewModels
{
    public class MovieViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public float? Mean { get; set; }
        public ICollection<CastOfMovieViewModel> CastOfMovieList { get; set; }
        //public ICollection<RatingOfMovie> MyProperty { get; set; }
    }
}
