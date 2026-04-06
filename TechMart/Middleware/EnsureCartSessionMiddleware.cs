using TechMart.Presentation.Modules.Cart;

namespace TechMart.Middleware;

public class EnsureCartSessionMiddleware
{
    private readonly RequestDelegate _next;

    public EnsureCartSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Session.IsAvailable)
        {
            var existing = context.Session.GetString(CartSessionIdProvider.SessionKey);
            if (string.IsNullOrEmpty(existing))
            {
                var id = Guid.NewGuid().ToString("N");
                context.Session.SetString(CartSessionIdProvider.SessionKey, id);
            }
        }

        await _next(context);
    }
}
