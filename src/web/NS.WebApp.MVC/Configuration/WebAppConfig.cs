using NS.WebApp.MVC.Extensions;

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
            app.UseDeveloperExceptionPage();
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

        app.UseMiddleware<ExceptionMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Catalog}/{action=Index}/{id?}");
    }
}
