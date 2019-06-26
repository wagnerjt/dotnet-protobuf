
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Dotnet.Proto.Helpers
{

    // The input formatter reading request body and mapping it to given data object.
    public class ProtobufInputFormatter : InputFormatter
    {
        static MediaTypeHeaderValue protoMediaType = MediaTypeHeaderValue.Parse("application/x-protobuf");

        public ProtobufInputFormatter()
        {
            SupportedMediaTypes.Add(protoMediaType.MediaType);
        }

        public override bool CanRead(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            MediaTypeHeaderValue requestContentType = null;
            MediaTypeHeaderValue.TryParse(request.ContentType, out requestContentType);

            if (requestContentType == null)
            {
                return false;
            }

            return requestContentType.MediaType.Equals(protoMediaType.MediaType);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            try
            {
                var request = context.HttpContext.Request;
                var obj = (IMessage)Activator.CreateInstance(context.ModelType);
                obj.MergeFrom(request.Body);
                
                // using (var ms = new MemoryStream(2048))
                // {
                //     await request.Body.CopyToAsync(ms);
                //     var content = ms.ToArray();
                //     // obj.MergeFrom(request.Body);
                //     // return await InputFormatterResult.SuccessAsync(content);
                //     obj.MergeFrom(content);
                // }

                return InputFormatterResult.SuccessAsync(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                return InputFormatterResult.FailureAsync();
            }
        }
    }

    // The output object mapping returned object to Protobuf-serialized response body.
    public class ProtobufOutputFormatter : OutputFormatter
    {
        static MediaTypeHeaderValue protoMediaType = MediaTypeHeaderValue.Parse("application/x-protobuf");

        public ProtobufOutputFormatter()
        {
            SupportedMediaTypes.Add(protoMediaType.MediaType);
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            if (context.Object != null && context.HttpContext.Request.ContentType.Equals(protoMediaType.MediaType))
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