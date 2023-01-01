using Event.Application;
using Event.Application.Functions.Candidate.IntegrationEvents;
using MQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEventApplication(new Uri(builder.Configuration["CandidateApiUrl"]));

builder.Services.AddIntegrationEventBusServices(builder.Configuration);
builder.Services.AddIntegrationEventBus(builder.Configuration);
builder.Services.AddIntegrationEventBusHandlers(builder.Configuration);

var app = builder.Build();

ConfigureIntegrationEventHandlers(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();

void ConfigureIntegrationEventHandlers(IServiceProvider serviceProvider)
{
    var eventBus = app.Services.GetRequiredService<IIntegrationEventBus>();
    eventBus.Subscribe<CreateCandidateApplicationIntegrationEvent, CreateCandidateApplicationIntegrationEventHandler>();
}