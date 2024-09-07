namespace Cms.Endpoints.AdminPanel.Auth
{
    public class AuthCheckAlready
    {
        private readonly RequestDelegate _next;

        public AuthCheckAlready(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            IConfiguration configuration;

            configuration = context.RequestServices.GetService<IConfiguration>();

            bool condition;

            if (configuration.GetSection("UnknownDate").Value == "0001-01-01T00:00:00.000Z")
            {
                condition = true;
            }
            else
            {
                DateTime date = DateTime.Parse(configuration.GetSection("UnknownDate").Value).ToLocalTime();
                DateTime now = DateTime.Now;

                condition = DateTime.Compare(date, now) > 0 ? true : false;
            }

            if (condition)
            {
                // If condition is true, continue processing the request
                await _next(context);
            }
            else
            {
                // If condition is false, return a failure response
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Panel Admin Not Available ): ");
            }
        }
    }
}
