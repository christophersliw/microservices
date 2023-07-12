using Candidate.API.Installers;
using Candidate.API.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Install<Program>(builder.Configuration);
builder.Services.Install<Candidate.Persistence.EF.MarkerAssembly>(builder.Configuration);
builder.Services.Install<Candidate.Application.MarkAssembly>(builder.Configuration);

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var swaggerOptions = new SwaggerOptions();
    builder.Configuration.Bind(nameof(SwaggerOptions), swaggerOptions);

    app.UseSwagger(o => { o.RouteTemplate = swaggerOptions.JsonRoute; });
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
       // o.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    
    });

}

//zakomentowac ze wzgledu na problemy z dokerem
app.UseHttpsRedirection();
app.UseAuthentication();
//app.UseAuthorization();


app.MapControllers();

app.Run();
