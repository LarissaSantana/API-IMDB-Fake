using IMDb.Domain.Core.Messages;
using IMDb.Domain.Enums;

namespace IMDb.Domain.Commands.Movie
{
    public class AddCastCommand : Command
    {
        public string Name { get; set; }
        public CastType CastType { get; set; }

        public AddCastCommand(string name, CastType castType)
        {
            Name = name;
            CastType = castType;
        }
    }
}
