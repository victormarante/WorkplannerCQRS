using System.Reflection.Metadata;

namespace WorkplannerCQRS.API.Domain
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }

        public string Message { get; private set; }

        public T Data { get; private set; }

        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message ?? string.Empty;
            Data = data;
        }

        /// <summary>
        /// Returns failed ApiResponse with message indicating what went wrong with the request
        /// </summary>
        /// <param name="message">Error message indicating what went wrong with the request</param>
        public ApiResponse(string message) : this(false, message, default(T))
        {
        }

        /// <summary>
        /// Return successful ApiResponse with data of type T
        /// </summary>
        /// <param name="data">Generic parameter T</param>
        public ApiResponse(T data) : this(true, string.Empty, data)
        {
        }
    }
}