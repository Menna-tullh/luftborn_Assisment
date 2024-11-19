
namespace Luftborn.Services.Common
{
    public class Response
    {
       
        public Response()
        {
            Errors = new();
        }
     
        public static Response Success(string message = default!)
        {
            return new Response
            {
                Succeeded = true,
                Message = message
            };
        }
        public static Response Fail(string message)
        {
            return new Response
            {
                Succeeded = false,
                Message = message
            };
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; } = null!;

        public List<string> Errors { get; set; }
    }
}
