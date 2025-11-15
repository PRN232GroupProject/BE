
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Metadata
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data, string message = "Success", int statusCode = 200)
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
        }

        public static ServiceResult<T> Failure(string message, int statusCode = 400)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Data = default
            };
        }
    }
}