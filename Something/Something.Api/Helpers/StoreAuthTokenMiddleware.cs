using System.Threading.Tasks;
using Byui.Something.Infrastructure.Common;
using Microsoft.AspNetCore.Http;

namespace Byui.Something.Api.Helpers
{
    public class StoreAuthTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private const string Token = "token";

        public StoreAuthTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserAuthToken authToken)
        {
            if (context.Request.Headers.TryGetValue(Token, out var temp))
            {
                authToken.Token = temp;
            }

            await _next(context);
        }
    }
}
