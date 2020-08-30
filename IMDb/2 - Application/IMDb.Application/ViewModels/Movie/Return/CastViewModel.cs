using IMDb.Domain.Enums;
using System;

namespace IMDb.Application.ViewModels.Movie.Return
{
    public class CastViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CastType CastType { get; set; }
    }
}
