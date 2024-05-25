using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Service.Extensions
{
    [Serializable]
    public class ServiceExeption : Exception
    {
        public ServiceExeption(HttpStatusCode statusCode, string? message = "Error") : base(message)
        {
            Data.Add("StatusCode", statusCode);
        }
    }
}
