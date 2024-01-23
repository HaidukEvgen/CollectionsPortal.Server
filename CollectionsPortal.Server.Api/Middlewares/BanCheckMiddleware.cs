using CollectionsPortal.Server.BusinessLayer.Exceptions;
using CollectionsPortal.Server.BusinessLayer.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CollectionsPortal.Server.Api.Middlewares
{
    public class BanCheckMiddleware(RequestDelegate next)
    {
        private string BadTokenMessage = "Bad token claims";

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var user = context.User;

            if (!user.Identity.IsAuthenticated)
            {
                await next(context);
                return;
            }

            var username = user.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (string.IsNullOrEmpty(username))
            {
                await WriteErrorResponce(context, StatusCodes.Status401Unauthorized, BadTokenMessage);
                return;
            }

            var userExists = await userService.IsUserExist(username);

            if (!userExists)
            {
                var url = context.Request.Path.Value;
                if (!LoginOrRegisterAttempt(url))
                {
                    await WriteErrorResponce(context, StatusCodes.Status403Forbidden, UserDeletedException.ErrorMessage);
                    return;
                }
                await next(context);
                return;
            }

            var isBanned = await userService.IsUserBanned(username);

            if (isBanned)
            {
                await WriteErrorResponce(context, StatusCodes.Status403Forbidden, UserBannedException.ErrorMessage);
                return;
            }

            await next(context);
        }

        private static bool LoginOrRegisterAttempt(string? url)
        {
            return (url.EndsWith("users/login") || url.EndsWith("users/register"));
        }

        private async Task WriteErrorResponce(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(
                new ErrorDetails()
                {
                    StatusCode = statusCode,
                    Message = message
                }.ToString()
            );
        }
    }
}
