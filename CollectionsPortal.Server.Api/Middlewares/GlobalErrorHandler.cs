using CollectionsPortal.Server.BusinessLayer.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CollectionsPortal.Server.Api.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    RegisterException => (int)HttpStatusCode.BadRequest,
                    LoginException => (int)HttpStatusCode.Unauthorized,
                    UserBannedException => (int)HttpStatusCode.Forbidden,
                    UserDeletedException => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                await response.WriteAsync(
                    new ErrorDetails()
                    {
                        StatusCode = response.StatusCode,
                        Message = error.Message
                    }.ToString()
                );
            }
        }
    }
}
