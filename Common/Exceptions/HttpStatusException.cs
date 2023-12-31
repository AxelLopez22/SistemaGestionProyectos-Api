﻿using System.Net;

namespace Api_ProjectManagement.Common.Exceptions
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpStatusException(HttpStatusCode statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }
    }
}
