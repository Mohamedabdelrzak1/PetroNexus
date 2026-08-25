using System.Collections.Generic;

namespace Shared.Common
{
    /// <summary>
    /// Unified API response wrapper for all endpoints.
    /// </summary>
    /// <typeparam name="T">Type of the response data payload.</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        /// <summary>Creates a successful response with data.</summary>
        public static ApiResponse<T> SuccessResult(T? data, string message = "Operation completed successfully.")
            => new() { Success = true, Message = message, Data = data, Errors = null };

        /// <summary>Creates a failure response with error messages.</summary>
        public static ApiResponse<T> FailureResult(string message, List<string>? errors = null)
            => new() { Success = false, Message = message, Data = default, Errors = errors ?? new List<string>() };

        /// <summary>Creates a failure response with a single error message.</summary>
        public static ApiResponse<T> FailureResult(string message, string error)
            => new() { Success = false, Message = message, Data = default, Errors = new List<string> { error } };
    }

    /// <summary>
    /// Non-generic helper for ApiResponse when no data is returned.
    /// </summary>
    public class ApiResponse : ApiResponse<object>
    {
        public static new ApiResponse SuccessResult(string message = "Operation completed successfully.")
            => new() { Success = true, Message = message, Data = null, Errors = null };

        public static new ApiResponse FailureResult(string message, List<string>? errors = null)
            => new() { Success = false, Message = message, Data = null, Errors = errors ?? new List<string>() };

        public static new ApiResponse FailureResult(string message, string error)
            => new() { Success = false, Message = message, Data = null, Errors = new List<string> { error } };
    }
}