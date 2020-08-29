using System;

namespace IMDb.Application.ViewModels.Return
{
    public class MovieWithRatingViewModel : BaseMovieViewModel
    {
        public int? NumberOfVotes { get; set; }
    }
}
