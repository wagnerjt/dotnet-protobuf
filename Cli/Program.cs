using System;
using System.IO;
using System.Threading.Tasks;
using Google.Protobuf;
using Dotnet.Proto.Models;
using Dotnet.Proto.CLI.Client;

namespace Dotnet.Proto.CLI
{
    class Program
    {
        public static void Main(string[] args)
        {
            var bytes = File.ReadAllBytes("./x.bin");
            var obj = (IMessage)Activator.CreateInstance(typeof(Person));
            obj.MergeFrom(bytes);

            HttpPostRequest("api/values/person", obj).GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");

            var person = (Person) obj;
            Console.WriteLine(person.Email);
        }



        private static async Task HttpPostRequest(string endpoint, IMessage message)
        {
            IClient protoClient = new ProtoClient("https://localhost:5001/");

            var result = await protoClient.Post<Person>(endpoint, message);
            Console.WriteLine(result.ToString());
        }
    }
}
