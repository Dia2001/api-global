using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.DTO.Response
{
    public class ResponseData<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool? Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public T? Data { get; set; }
    }
    public class ResponseData
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool? Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public bool? Data = null;
    }

    public class SingleId
    {
        public Guid Id { get; set; }
    }
    public class SingleId<T>
    {
        public T Id { get; set; }
    }

    public class DataList<T>
    {
        public List<T> Items { get; set; }
        public int? TotalRecord { get; set; }
    }
}
