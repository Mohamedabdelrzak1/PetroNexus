using Microsoft.AspNetCore.Mvc;
using Shared.Common;

namespace Presentation.Controllers
{
    /// <summary>
    /// Helper methods for returning unified API responses from controllers.
    /// </summary>
    public static class ControllerHelper
    {
        // ─── 200 OK ──────────────────────────────────────────────────────────
        public static ActionResult<ApiResponse<T>> OkResponse<T>(this ControllerBase controller, T? data, string message = "Retrieved successfully.")
            => controller.Ok(ApiResponse<T>.SuccessResult(data, message));

        public static ActionResult<ApiResponse> OkResponse(this ControllerBase controller, string message = "Operation completed successfully.")
            => controller.Ok(ApiResponse.SuccessResult(message));

        // ─── 201 Created ─────────────────────────────────────────────────────
        public static ActionResult<ApiResponse<T>> CreatedResponse<T>(this ControllerBase controller, string actionName, object routeValues, T? data, string message = "Created successfully.")
            => controller.CreatedAtAction(actionName, routeValues, ApiResponse<T>.SuccessResult(data, message));

        // ─── 400 BadRequest ──────────────────────────────────────────────────
        public static ActionResult<ApiResponse<T>> BadRequestResponse<T>(this ControllerBase controller, string message, List<string>? errors = null)
            => controller.BadRequest(ApiResponse<T>.FailureResult(message, errors));

        public static ActionResult<ApiResponse> BadRequestResponse(this ControllerBase controller, string message, List<string>? errors = null)
            => controller.BadRequest(ApiResponse.FailureResult(message, errors));

        // ─── 401 Unauthorized ────────────────────────────────────────────────
        // ─── 401 Unauthorized ────────────────────────────────────────────────
        public static ActionResult<ApiResponse<T>> UnauthorizedResponse<T>(
            this ControllerBase controller,
            string message = "Unauthorized access.")
            => new UnauthorizedObjectResult(ApiResponse<T>.FailureResult(message));

        public static ActionResult<ApiResponse> UnauthorizedResponse(
            this ControllerBase controller,
            string message = "Unauthorized access.")
            => new UnauthorizedObjectResult(ApiResponse.FailureResult(message));

        // ─── 404 NotFound ────────────────────────────────────────────────────
        public static ActionResult<ApiResponse<T>> NotFoundResponse<T>(this ControllerBase controller, string message)
            => controller.NotFound(ApiResponse<T>.FailureResult(message));

        public static ActionResult<ApiResponse> NotFoundResponse(this ControllerBase controller, string message)
            => controller.NotFound(ApiResponse.FailureResult(message));

        // ─── 409 Conflict ────────────────────────────────────────────────────
        public static ActionResult<ApiResponse<T>> ConflictResponse<T>(this ControllerBase controller, string message)
            => controller.Conflict(ApiResponse<T>.FailureResult(message));

        public static ActionResult<ApiResponse> ConflictResponse(this ControllerBase controller, string message)
            => controller.Conflict(ApiResponse.FailureResult(message));

        // ─── 500 InternalServerError ────────────────────────────────────────
        public static ActionResult<ApiResponse<T>> InternalServerErrorResponse<T>(this ControllerBase controller, string message = "Internal server error.")
            => controller.StatusCode(500, ApiResponse<T>.FailureResult(message));

        public static ActionResult<ApiResponse> InternalServerErrorResponse(this ControllerBase controller, string message = "Internal server error.")
            => controller.StatusCode(500, ApiResponse.FailureResult(message));
    }
}