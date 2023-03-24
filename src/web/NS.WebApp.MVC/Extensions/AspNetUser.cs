using System.Security.Claims;

namespace NS.WebApp.MVC.Extensions;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AspNetUser(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;

    public string Name => _contextAccessor.HttpContext.User.Identity.Name;

    public IEnumerable<Claim> GetClaims() => _contextAccessor.HttpContext.User.Claims;

    public string GetEmail() => IsAuthenticated() ? _contextAccessor.HttpContext.User.GetUserEmail() : string.Empty;

    public HttpContext GetHttpContext() => _contextAccessor.HttpContext;

    public Guid GetId() => IsAuthenticated() ? Guid.Parse(_contextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;

    public string GetToken() => IsAuthenticated() ? _contextAccessor.HttpContext.User.GetUserToken() : string.Empty;

    public bool HasRole(string role) => _contextAccessor.HttpContext.User.IsInRole(role);

    public bool IsAuthenticated() => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;    
}

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal is null) throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst("sub");
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal is null) throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst("email");
        return claim?.Value;
    }

    public static string GetUserToken(this ClaimsPrincipal principal)
    {
        if (principal is null) throw new ArgumentException(nameof(principal));

        var claim = principal.FindFirst("JWT");
        return claim?.Value;
    }
}
