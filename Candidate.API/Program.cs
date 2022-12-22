using Candidate.Application;
using Candidate.Persistence.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCandidateApplication(new Uri(builder.Configuration["RecruitmentApiUrl"]));

builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddIntegrationEventBusServices(builder.Configuration);
builder.Services.AddIntegrationEventBus(builder.Configuration);

builder.Services.AddPersistanceEFServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();