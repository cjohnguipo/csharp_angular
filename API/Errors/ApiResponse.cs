using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You made a Bad Request",
                401 => "Sorry, you are not Authorized",
                404 => "Resource Not Found",
                405 => "Request Method Not Allowed",
                500 => "Server Error",
                501 => "Not Allowed",
                _ => null
            };
        }

    }
}