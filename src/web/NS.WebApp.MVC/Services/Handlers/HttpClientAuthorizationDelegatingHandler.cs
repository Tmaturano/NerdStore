using NS.WebApp.MVC.Extensions;
using System.Net.Http.Headers;

namespace NS.WebApp.MVC.Services.Handlers;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IUser _user;

    public HttpClientAuthorizationDelegatingHandler(IUser user)
    {
        _user = user;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.GetHttpContext().Request.Headers.Authorization;
        
        if(!string.IsNullOrEmpty(authorizationHeader) ) 
            request.Headers.Add("Authorization", new List<string> { authorizationHeader });

        var token = _user.GetToken();
        
        if (token is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return base.SendAsync(request, cancellationToken);
    }
}
