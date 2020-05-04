using System;
using System.Net;

namespace SharedLibrary.Models
{
    public class CustomException : Exception
    {
        public HttpStatusCode ErrorCode { get; set; }
    }
}