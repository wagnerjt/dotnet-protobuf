
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Dotnet.Proto.CLI {
    public class Client : IClient
    {
        private string baseUri;

        public Client(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public Task<IMessage> Get(string endpoint, IMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<IMessage> Get(CancellationToken ct, string endpoint, IMessage message)
        {
            throw new NotImplementedException();
        }

        public async Task<IMessage> Post<T>(string endpoint, IMessage message)
        {
            HttpClient client = new HttpClient();
            // client.DefaultRequestHeaders.Add("Content-Type", );

            ByteArrayContent payload = new ByteArrayContent(message.ToByteArray());
            payload.Headers.ContentType = new MediaTypeHeaderValue("application/x-protobuf");

            var content = message.ToByteArray();
            var response = await client.PostAsync(baseUri + endpoint, payload);

            IMessage result = (IMessage) Activator.CreateInstance(typeof(T));

            if(response.IsSuccessStatusCode) {
                Console.WriteLine("Successful");
                result.MergeFrom(await response.Content.ReadAsByteArrayAsync());
                Console.WriteLine("Successful");
            }

            return result;
        }

        public Task<IMessage> Post(CancellationToken ct, string endpoint, IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
