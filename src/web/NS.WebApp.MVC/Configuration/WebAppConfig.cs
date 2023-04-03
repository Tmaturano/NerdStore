using Microsoft.AspNetCore.Localization;
using NS.WebApp.MVC.Extensions;
using System.Globalization;

namespace NS.WebApp.MVC.Configuration;

public static class WebAppConfig
{
    public static void AddMvcConfiguration(this WebApplicationBuilder app)
    {
        app.Configuration.SetBasePath(app.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (app.Environment.IsDevelopment())
            app.Configuration.AddUserSecrets<Program>();

        app.Services.AddControllersWithViews();
        app.Services.Configure<AppSettings>(app.Configuration);
    }

    public static void UseMvcConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
        }
        else
        {
            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfiguration();

        var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseMiddleware<ExceptionMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Catalog}/{action=Index}/{id?}");
    }
}
