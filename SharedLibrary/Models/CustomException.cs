using System;
using System.Net;

namespace SharedLibrary.Models
{
    public class CustomException : Exception
    {
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public HttpStatusCode ErrorCode { get; set; }

        /// <summary>
        /// 是否已登入
        /// </summary>
        public bool IsAuthenticated { get; set; }
    }
}