using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NS.Identity.API.Data;
using NS.WebApi.Core.Identity;

namespace NS.Identity.API.Configuration;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.AddJwtConfiguration();

        return builder;
    }   
}
