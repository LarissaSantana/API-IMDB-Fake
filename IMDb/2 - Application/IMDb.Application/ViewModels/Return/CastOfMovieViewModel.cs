using System;

namespace IMDb.Application.ViewModels.Return
{
    public class CastOfMovieViewModel
    {
        public Guid Id { get; set; }
        public CastViewModel Cast { get; set; }
    }
}