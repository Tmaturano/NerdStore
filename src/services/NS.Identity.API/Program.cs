using NS.Identity.API.Configuration;

#region Configure Services
var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerConfiguration();
builder.AddApiConfiguration();
builder.AddIdentityConfiguration();

var app = builder.Build();
#endregion

#region Configure Pipeline
app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.MapControllers();

app.Run();

#endregion