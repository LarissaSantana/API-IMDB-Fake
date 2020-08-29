using IMDb.Domain.Enums;
using System;

namespace IMDb.Application.ViewModels
{
    public class CastOfMovieViewModel
    {
        public Guid CastOfMovieId { get; set; }
        public string Name { get; set; }
        public CastType CastType { get; set; }
    }
}