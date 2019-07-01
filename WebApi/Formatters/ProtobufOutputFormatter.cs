using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Dotnet.Proto.Formatters
{
    // The output object mapping returned object to Protobuf-serialized response body.
    public class ProtobufOutputFormatter : OutputFormatter
    {

        public ProtobufOutputFormatter()
        {
            SupportedMediaTypes.Add(Protobuf.MediaType);
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            if (context.Object != null && context.HttpContext.Request.ContentType.Equals(Protobuf.MediaType))
            {
                // Check whether the given object is a proto-generated object
                return context.ObjectType.GetInterfaces()
                    .Where(i => i.IsGenericTypeDefinition && i == typeof(IMessage<>)) != null;
            }
            return false;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            // Proto-encode
            var protoObj = context.Object as IMessage;
            var serialized = protoObj.ToByteArray();

            return response.Body.WriteAsync(serialized, 0, serialized.Length);
        }
    }
}
