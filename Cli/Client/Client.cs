
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Dotnet.Proto.CLI.Client
{
    public class ProtoClient : IClient
    {
        public static MediaTypeHeaderValue ContentType = new MediaTypeHeaderValue("application/x-protobuf");

        private string baseUri;
        private HttpClient client;

        public ProtoClient(string baseUri)
        {
            this.baseUri = baseUri;
            this.client = new HttpClient();
        }

        public async Task<T> Get<T>(string endpoint) where T : IMessage
        {
            var response = await client.GetAsync(baseUri + endpoint);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> Get<T>(CancellationToken ct, string endpoint) where T : IMessage
        {
            var response = await client.GetAsync(baseUri + endpoint, ct);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> Post<T>(string endpoint, IMessage message) where T : IMessage
        {
            var payload = SerializePayload(message);
            var response = await client.PostAsync(baseUri + endpoint, payload);

            return await DeserializeResponse<T>(response);
        }

        public async Task<T> Post<T>(CancellationToken ct, string endpoint, IMessage message) where T : IMessage
        {
            var payload = SerializePayload(message);
            var response = await client.PostAsync(baseUri + endpoint, payload, ct);

            return await DeserializeResponse<T>(response);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response) where T : IMessage
        {
            IMessage result = (IMessage) Activator.CreateInstance(typeof(T));

            if(response.IsSuccessStatusCode) {
                result.MergeFrom(await response.Content.ReadAsByteArrayAsync());
            }

            return (T) result;
        }

        private ByteArrayContent SerializePayload(IMessage message)
        {
            ByteArrayContent payload = new ByteArrayContent(message.ToByteArray());
            payload.Headers.ContentType = ProtoClient.ContentType;

            return payload;
        }
    }
}
