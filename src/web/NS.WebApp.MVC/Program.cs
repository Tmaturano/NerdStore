using NS.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddIdentityConfiguration();
builder.AddMvcConfiguration();
builder.Services.AddControllersWithViews();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
