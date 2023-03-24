using System.Security.Claims;

namespace NS.WebApp.MVC.Extensions;

public interface IUser
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
