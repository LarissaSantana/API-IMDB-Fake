using System;
using System.ComponentModel.DataAnnotations;

namespace IMDb.Application.ViewModels.Movie.Add
{
    public class AddRatingOfMovieViewModel
    {
        [Required(ErrorMessage = "The {0} is required")]
        [Range(0, 4, ErrorMessage = "O Rate deve estar entre 0 e 4!")]
        public int Rate { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public Guid MovieId { get; set; }
    }
}
