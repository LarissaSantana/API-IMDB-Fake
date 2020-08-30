using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDb.Application.ViewModels.Movie.Add
{
    public class AddMovieViewModel
    {
        [Required(ErrorMessage = "The {0} is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public Genre Genre { get; set; }

        public List<Guid> CastIds { get; set; }
    }
}
