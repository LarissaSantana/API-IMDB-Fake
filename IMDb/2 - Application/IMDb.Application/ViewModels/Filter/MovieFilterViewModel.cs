using IMDb.Application.ViewModels.Filter;
using IMDb.Domain.Enums;
using System.Collections.Generic;

namespace IMDb.Application.ViewModels.Filters
{
    public class MovieFilterViewModel
    {
        public string Title { get; set; }
        public Genre? Genre { get; set; }
        public List<CastFilterViewModel> CastList { get; set; }
    }
}
