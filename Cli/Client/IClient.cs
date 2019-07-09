using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Dotnet.Proto.CLI.Client
{
    public interface IClient
    {
        Task<T> Get<T>(string endpoint) where T : IMessage;
        Task<T> Get<T>(CancellationToken ct, string endpoint) where T : IMessage;
        Task<T> Post<T>(string endpoint, IMessage message) where T : IMessage;
        Task<T> Post<T>(CancellationToken ct, string endpoint, IMessage message) where T : IMessage;
    }
}
