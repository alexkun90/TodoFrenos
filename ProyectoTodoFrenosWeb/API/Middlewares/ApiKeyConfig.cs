namespace API.Middlewares
{
    public class ApiKeyConfig
    {
        private readonly RequestDelegate next;
        private const string ApiKeyName = "ApiKey";
        public ApiKeyConfig(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey)) 
            {
                context.Response.StatusCode = 401; 
                await context.Response.WriteAsync("No posee los permisos necesarios");
                return;
            }

            var apiKey = config.GetValue<string>("ApiKey");
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 403; 
                await context.Response.WriteAsync("Acceso Denegado");
                return;
            }

            await next(context);
        }
    }
}
