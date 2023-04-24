using NS.Clients.API.Configuration;
using NS.WebApi.Core.Identity;

#region Configure Services
var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerConfiguration();
builder.AddApiConfiguration();
builder.AddJwtConfiguration();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.RegisterServices();

var app = builder.Build();
#endregion

#region Configure Pipeline
app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.MapControllers();

app.Run();

#endregion
