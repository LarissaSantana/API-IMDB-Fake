using IMDb.Domain.Enums;

namespace IMDb.Application.ViewModels.Movie.Add
{
    public class AddCastViewModel
    {
        public string Name { get; set; }
        public CastType CastType { get; set; }
    }
}
