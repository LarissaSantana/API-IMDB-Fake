using IMDb.Domain.Core.Messages;
using System.Threading.Tasks;

namespace IMDb.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T comando) where T : Command;
    }
}
