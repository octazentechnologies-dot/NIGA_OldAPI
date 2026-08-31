using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class ErrorResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
