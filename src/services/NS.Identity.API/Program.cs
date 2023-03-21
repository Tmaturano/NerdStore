using NS.Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerConfiguration();
builder.AddApiConfiguration();
builder.AddIdentityConfiguration();

var app = builder.Build();
app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.Run();