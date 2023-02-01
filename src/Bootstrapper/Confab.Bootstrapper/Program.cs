using Confab.Modules.Conferences.Api;
using Confab.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure();
builder.Services.AddConferences();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseInfrastructure();

app.Run();