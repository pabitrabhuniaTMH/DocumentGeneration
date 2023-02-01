using System.Net;

namespace Domain.Models
{
    public class ApiResponse
    {
        public long Id { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
    }

    public class ResponseValue<T>
    {
        public T? ResponseData { get; set; }
    }
    public class TemplateResponseValue
    {
        public string? TemplateId { get; set; }
        public string? TemplateName { get; set; }
    }
}
