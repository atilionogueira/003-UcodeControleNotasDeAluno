using System.Text.Json.Serialization;

namespace Ucode.Core.Responses
{
    public class Response<TData>
    {       
        private readonly int _code;

        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore] // impedi que o IsSuccess seja exibido na tela
        public bool IsSuccess => _code >= 200 && _code <= 299;

        [JsonConstructor]
        public Response() => _code = Configuration.DefaultStatusCode;
        
        public Response(TData? data, int code = 200, string? message = null)
        {
            Data = data;
            _code = Configuration.DefaultStatusCode;
            Message = message;
        }          

    }
}
