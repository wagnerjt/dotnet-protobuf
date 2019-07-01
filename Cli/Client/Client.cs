
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

        public async Task<IMessage> Get<T>(string endpoint)
        {
            var response = await client.GetAsync(baseUri + endpoint);
            return await DeserializeResponse<T>(response);
        }

        public async Task<IMessage> Get<T>(CancellationToken ct, string endpoint)
        {
            var response = await client.GetAsync(baseUri + endpoint, ct);
            return await DeserializeResponse<T>(response);
        }

        public async Task<IMessage> Post<T>(string endpoint, IMessage message)
        {
            var payload = SerializePayload(message);
            var response = await client.PostAsync(baseUri + endpoint, payload);

            return await DeserializeResponse<T>(response);
        }

        public async Task<IMessage> Post<T>(CancellationToken ct, string endpoint, IMessage message)
        {
            var payload = SerializePayload(message);
            var response = await client.PostAsync(baseUri + endpoint, payload, ct);

            return await DeserializeResponse<T>(response);
        }

        private async Task<IMessage> DeserializeResponse<T>(HttpResponseMessage response)
        {
            IMessage result = (IMessage) Activator.CreateInstance(typeof(T));

            if(response.IsSuccessStatusCode) {
                result.MergeFrom(await response.Content.ReadAsByteArrayAsync());
            }

            return result;
        }

        private ByteArrayContent SerializePayload(IMessage message)
        {
            ByteArrayContent payload = new ByteArrayContent(message.ToByteArray());
            payload.Headers.ContentType = ProtoClient.ContentType;

            return payload;
        }
    }
}
