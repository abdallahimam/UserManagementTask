

namespace UserManagementTask.Application.DTOs
{
    public class Result<T> where T : class
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }

        public static Result<T> SuccessResponse(T data, int statusCode = 200, string? message = null)
        {
            return new Result<T> { Success = true, Data = data, Message = message, StatusCode = statusCode };
        }
        public static Result<T> FailResponse(string message, int statusCode = 400)
        {
            return new Result<T> { Success = false, Message = message, StatusCode = statusCode };
        }
    }
}
