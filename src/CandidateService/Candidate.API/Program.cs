using Candidate.Application.Installers;
using Candidate.Persistence.EF.Installers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//builder.Services.InstallServiceInAssembly(builder.Configuration, typeof(Program));
builder.Services.InstallerPersistenceEFServiceInAssembly(builder.Configuration);
builder.Services.InstallerApplicationServiceInAssembly(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//zakomentowac ze wzgledu na problemy z dokerem
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
