using NS.Catalog.API.Configuration;

#region Configure Services
var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerConfiguration();
builder.AddApiConfiguration();
builder.Services.RegisterServices();

var app = builder.Build();
#endregion

#region Configure Pipeline
app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.MapControllers();

app.Run();

#endregion
