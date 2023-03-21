namespace NS.Identity.API.Configuration;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (builder.Environment.IsDevelopment())
            builder.Configuration.AddUserSecrets<Program>();

        builder.Services.AddControllers();
        
        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())        
            app.UseDeveloperExceptionPage();        

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseIdentityConfiguration();

        app.MapControllers();

        return app;
    }
}
