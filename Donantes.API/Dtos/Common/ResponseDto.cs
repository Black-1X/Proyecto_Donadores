namespace Donantes.API.Dtos.Common
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }

        public bool Status { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }
    }
}