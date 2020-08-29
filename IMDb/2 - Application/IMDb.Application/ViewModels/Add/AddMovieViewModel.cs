﻿using IMDb.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDb.Application.ViewModels.Add
{
    public class AddMovieViewModel
    {
        //TODO: traduzir mensagens de erro
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Genre Genre { get; set; }

        [MinLength(2, ErrorMessage = "At least one item needs to be reported")]
        public List<AddCastViewModel> Casts { get; set; }
    }
}
