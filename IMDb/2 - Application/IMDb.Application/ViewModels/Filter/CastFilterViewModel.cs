using IMDb.Domain.Enums;

namespace IMDb.Application.ViewModels.Filter
{
    public class CastFilterViewModel
    {
        public string Name { get; set; }
        public CastType CastType { get; set; }
    }
}
