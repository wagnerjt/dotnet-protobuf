
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Dotnet.Proto.CLI.Client
{
    public interface IClient
    {
        Task<IMessage> Get<T>(string endpoint);
        Task<IMessage> Get<T>(CancellationToken ct, string endpoint);
        Task<IMessage> Post<T>(string endpoint, IMessage message);
        Task<IMessage> Post<T>(CancellationToken ct, string endpoint, IMessage message);
    }
}
