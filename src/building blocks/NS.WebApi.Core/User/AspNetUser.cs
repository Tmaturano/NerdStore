using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NS.WebApi.Core.User;

public class AspNetUser : IAspNetUser
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


