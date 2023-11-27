namespace RLUltimateApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKeyHeader;
        private readonly string _storedApiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration, string apiKeyHeader = "XApiKey")
        {
            _next = next;
            _apiKeyHeader = apiKeyHeader;
            _storedApiKey = configuration.GetValue<string>("StoredApiKey");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(_apiKeyHeader, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key was not provided");
                return;
            }

            if (!TimeConstantCompare(extractedApiKey, _storedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            await _next(context);
        }

        private bool TimeConstantCompare(string a, string b)
        {
            return string.Equals(a, b, StringComparison.Ordinal);
        }
    }

}
