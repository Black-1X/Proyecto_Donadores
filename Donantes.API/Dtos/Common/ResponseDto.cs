
using System.Text.Json.Serialization;

namespace Donantes.API.Dtos.Common
{
    public class ResponseDto<T>
    {
        [JsonIgnore]
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }
    }
}


 
        