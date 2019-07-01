using System.Net.Http.Headers;


// Super helpful Article Tero.
// https://tero.teelahti.fi/using-google-proto3-with-aspnet-mvc/
namespace Dotnet.Proto.Formatters
{
    public abstract class Protobuf
    {
        // public static MediaTypeHeaderValue MediaType = MediaTypeHeaderValue.Parse("application/x-protobuf");
        public const string MediaType = "application/x-protobuf";
    }
}
