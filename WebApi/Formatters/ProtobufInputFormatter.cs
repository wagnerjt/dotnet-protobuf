using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Dotnet.Proto.Formatters
{
    // The input formatter reading request body and mapping it to given data object.
    public class ProtobufInputFormatter : InputFormatter
    {

        public ProtobufInputFormatter()
        {
            SupportedMediaTypes.Add(Protobuf.MediaType);
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

            return SupportedMediaTypes.Contains(requestContentType.MediaType);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            try
            {
                var request = context.HttpContext.Request;
                var obj = (IMessage)Activator.CreateInstance(context.ModelType);
                obj.MergeFrom(request.Body);

                return InputFormatterResult.SuccessAsync(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                return InputFormatterResult.FailureAsync();
            }
        }
    }
}
