using Microsoft.AspNetCore.Http;
using TechMart.Presentation.Modules.Cart.Interfaces;

namespace TechMart.Presentation.Modules.Cart;

public class CartSessionIdProvider : ICartSessionIdProvider
{
    public const string SessionKey = "CartSessionId";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartSessionIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCartSessionId()
    {
        var session = _httpContextAccessor.HttpContext?.Session
            ?? throw new InvalidOperationException("Session is not available.");

        var id = session.GetString(SessionKey);
        if (string.IsNullOrEmpty(id))
            throw new InvalidOperationException("Cart session was not initialized. Ensure session middleware runs before MVC.");

        return id;
    }
}
