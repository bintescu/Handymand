using Handymand.Services;
using Handymand.Utilities.JWTUtils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Utilities
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService, IJWTUtils jWTUtils)
        {
            //Bearer -token-
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null && token.Equals("null") == false)
            {
                var userId = jWTUtils.ValidateJWTToken(token);

                if (userId != null)
                {
                    httpContext.Items["User"] = userService.GetById((int)userId);
                }

            }

            await _next(httpContext);


        }

    }
}
