using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Protobuf;

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

            // var payload = message.ToByteArray();

            // myHttpWebRequest.ContentLength = payload.Length;

            // Stream requestStream = myHttpWebRequest.GetRequestStream();
            // requestStream.Write(payload, 0, payload.Length);
            // requestStream.Close();

            // HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            // Stream responseStream = myHttpWebResponse.GetResponseStream();
            // var boj = (IMessage)Activator.CreateInstance(typeof(Person));
            // boj.MergeDelimitedFrom(responseStream);


            // StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

            // string pageContent = myStreamReader.ReadToEnd();

            // myStreamReader.Close();
            // responseStream.Close();

            // myHttpWebResponse.Close();
        }
    }
}
