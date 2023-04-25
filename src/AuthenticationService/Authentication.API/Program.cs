using Authentication.API.Installers;
using Authentication.API.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Install(builder.Configuration);

var app = builder.Build();

var swaggerOptions = new SwaggerOptions();
builder.Configuration.Bind(nameof(SwaggerOptions), swaggerOptions);

app.UseSwagger(o => { o.RouteTemplate = swaggerOptions.JsonRoute; });

app.UseSwaggerUI(o => { o.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });

app.UseHttpsRedirection();

app.MapControllers();

app.Run();