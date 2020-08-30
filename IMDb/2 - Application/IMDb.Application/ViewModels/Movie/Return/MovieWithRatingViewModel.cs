using System;

namespace IMDb.Application.ViewModels.Movie.Return
{
    public class MovieWithRatingViewModel : BaseMovieViewModel
    {
        public int? NumberOfVotes { get; set; }
    }
}
