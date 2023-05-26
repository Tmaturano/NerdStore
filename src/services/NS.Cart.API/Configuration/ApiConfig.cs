﻿using Microsoft.EntityFrameworkCore;
using NS.Catalog.API.Data;
using NS.WebApi.Core.Identity;

namespace NS.Cart.API.Configuration;

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

        builder.Services.AddDbContext<CartContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("All",
                policy => policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
        });

        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())        
            app.UseDeveloperExceptionPage();        

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();

        app.UseAuthConfiguration();

        return app;
    }
}
