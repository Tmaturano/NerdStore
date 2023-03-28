using System.Net;

namespace NS.WebApp.MVC.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
		try
		{
			await _next(context);
		}
		catch (CustomHttpResponseException ex)
		{
			HandleRequestExceptionAsync(context, ex);
		}
    }

    private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpResponseException ex)
    {
        if (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return;
        }

        context.Response.StatusCode = (int)ex.StatusCode;
    }
}
