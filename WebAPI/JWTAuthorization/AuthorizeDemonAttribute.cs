using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using JWT.Builder;
using JWT.Algorithms;

namespace WebAPI.JWTAuthorization
{
    public class AuthorizeDemonAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                string token = context.HttpContext.Request.Headers["Authorized"].ToString().Split(' ').Last();

                var json = JwtBuilder.Create()
                         .WithAlgorithm(new HMACSHA256Algorithm())
                         .WithSecret("super-secret-bar")
                         .MustVerifySignature()
                         .Decode(token);
            }
            catch
            {
                context.Result = new JsonResult(new { message = "Wrong token" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
