using ApiGlobal.Common;
using ApiGlobal.DTO.Response;
using ApiGlobal.Service.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiGlobal.WebAPI.Controllers.Base
{
    public class RolesAllow : AuthorizeAttribute
    {
        public RolesAllow(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
    public abstract class ControllerApiBase<TController> : Controller
    {
        protected ILogger<TController> _logger;
        protected ControllerApiBase(ILogger<TController> logger)
        {
            _logger = logger;
        }

        [NonAction]
        protected IActionResult MyJsonResponse(HttpStatusCode statusCode, ResponseData result)
        {
            var jsonResult = Json(result);
            jsonResult.StatusCode = (int)statusCode;
            return jsonResult;
        }

        [NonAction]
        protected IActionResult MyJsonResponse<T>(HttpStatusCode statusCode, ResponseData<T> result)
        {
            var jsonResult = Json(result);
            jsonResult.StatusCode = (int)statusCode;
            return jsonResult;
        }

        [NonAction]
        protected IActionResult Success<T>(T result)
        {
            return MyJsonResponse<T>(HttpStatusCode.OK, new ResponseData<T>
            {
                StatusCode = HttpStatusCode.OK,
                Success = true,
                Message = null,
                Data = result
            });
        }

        [NonAction]
        protected IActionResult Created<T>(T result)
        {
            return MyJsonResponse<T>(HttpStatusCode.Created, new ResponseData<T>
            {
                StatusCode = HttpStatusCode.Created,
                Success = true,
                Message = null,
                Data = result
            });
        }

        [NonAction]
        protected IActionResult BadRequest(string? Message = null)
        {
            return MyJsonResponse(HttpStatusCode.BadRequest, new ResponseData
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                Message = Message
            });
        }

        [NonAction]
        protected IActionResult NotFoundData(string? Message = null)
        {
            return MyJsonResponse(HttpStatusCode.NotFound, new ResponseData
            {
                StatusCode = HttpStatusCode.NotFound,
                Success = false,
                Message = Message
            });
        }

        [NonAction]
        protected IActionResult Conflict(string? Message = null)
        {
            return MyJsonResponse(HttpStatusCode.Conflict, new ResponseData
            {
                StatusCode = HttpStatusCode.Conflict,
                Success = false,
                Message = Message
            });
        }

        [NonAction]
        protected IActionResult ServerError(string? Message = null)
        {
            return MyJsonResponse(HttpStatusCode.BadGateway, new ResponseData
            {
                StatusCode = HttpStatusCode.BadGateway,
                Success = false,
                Message = Message
            });
        }

        [NonAction]
        protected IActionResult Forbidden(string? Message = null)
        {
            return MyJsonResponse(HttpStatusCode.Forbidden, new ResponseData
            {
                StatusCode = HttpStatusCode.BadGateway,
                Success = false,
                Message = Message
            });
        }

        [NonAction]
        protected IActionResult HandleError(Exception ex)
        {
            if (ex is ServiceExeption)
            {
                HttpStatusCode statusCode = (HttpStatusCode)ex.Data["StatusCode"];
                switch (statusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return BadRequest(ex.Message);
                    case HttpStatusCode.Forbidden:
                        return Forbidden(ex.Message);
                    case HttpStatusCode.Conflict:
                        return Conflict(ex.Message);
                    case HttpStatusCode.NotFound:
                        return NotFoundData(ex.Message);
                    default: return BadRequest(ex.Message);
                }
            }
            else
            {
                return MyJsonResponse(HttpStatusCode.BadGateway, new ResponseData
                {
                    StatusCode = HttpStatusCode.BadGateway,
                    Success = false,
                    Message = ErrorMessage.BAD_GATEWAY
                });
            }

        }
    }
}
