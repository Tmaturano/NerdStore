using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NS.WebApi.Core.User;

public interface IAspNetUser
{
    string Name { get; }
    Guid GetId();
    string GetEmail();
    string GetToken();
    bool IsAuthenticated();
    bool HasRole(string role);
    IEnumerable<Claim> GetClaims();
    HttpContext GetHttpContext();
}
