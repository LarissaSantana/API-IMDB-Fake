using IMDb.Domain.Enums;
using System;
using System.Collections.Generic;

namespace IMDb.Application.ViewModels.Filters
{
    public class MovieFilterViewModel
    {
        public string Title { get; set; }
        public Genre? Genre { get; set; }
        public List<Guid> CastIds { get; set; }
    }
}
