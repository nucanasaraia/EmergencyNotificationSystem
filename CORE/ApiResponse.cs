using System.Net;

namespace EmergencyNotifRespons.CORE
{
    public class ApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
