namespace Cms.Endpoints.Admin.Token
{
    public class CheckJwtAlready
    {
        private readonly RequestDelegate _next;

        public CheckJwtAlready(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Your condition check logic here
            bool condition = true; // Replace with your actual condition

            if (condition)
            {
                // If condition is true, continue processing the request
                await _next(context);
            }
            else
            {
                // If condition is false, return a failure response
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Service Is Not Available ): ");
            }
        }
    }
}
