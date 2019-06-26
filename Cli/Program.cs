using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Protobuf;
using Dotnet.Proto.Models;

namespace Dotnet.Proto.CLI
{
    class Program
    {
        public static void Main(string[] args)
        {
            var bytes = File.ReadAllBytes("./x.bin");
            var obj = (IMessage)Activator.CreateInstance(typeof(Person));
            obj.MergeFrom(bytes);

            HttpPostRequest("https://localhost:5001/api/values/person", obj).GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");

            var person = (Person) obj;
            Console.WriteLine(person.Email);
        }



        private static async Task HttpPostRequest(string url, IMessage message)
        {
            HttpClient client = new HttpClient();
            // client.DefaultRequestHeaders.Add("Content-Type", );

            ByteArrayContent payload = new ByteArrayContent(message.ToByteArray());
            payload.Headers.ContentType = new MediaTypeHeaderValue("application/x-protobuf");

            var content = message.ToByteArray();
            var response = await client.PostAsync(url, payload);
            if(response.IsSuccessStatusCode) {
                Console.WriteLine("Successful");
                var boj = (IMessage)Activator.CreateInstance(typeof(Person));
                boj.MergeFrom(await response.Content.ReadAsByteArrayAsync());
                Console.WriteLine("Successful");
            }
        }
    }
}
