
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Dotnet.Proto.CLI
{
    public interface IClient
    {
        Task<IMessage> Get(string endpoint, IMessage message);
        Task<IMessage> Get(CancellationToken ct, string endpoint, IMessage message);
        Task<IMessage> Post<T>(string endpoint, IMessage message);
        Task<IMessage> Post(CancellationToken ct, string endpoint, IMessage message);
    }
}
