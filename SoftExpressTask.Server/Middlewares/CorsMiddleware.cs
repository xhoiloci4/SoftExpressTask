using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SoftExpressTask.Server.Middlewares
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow all origins (you can restrict it to specific domains)
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            // Allow specific methods (GET, POST, PUT, DELETE, etc.)
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");

            // Allow specific headers
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

            // Allow credentials (optional)
            // context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");

            // Handle preflight requests (OPTIONS)
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                return;
            }

            await _next(context);
        }
    }
}
