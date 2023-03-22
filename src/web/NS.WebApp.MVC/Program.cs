using NS.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddIdentityConfiguration();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
