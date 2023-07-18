namespace Api_ProjectManagement.Common.DTOs.Response
{
    public class ModelResponse
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string? Message { get; set; }
    }
}
