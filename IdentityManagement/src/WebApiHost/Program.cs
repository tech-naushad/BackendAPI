using IdentityManagement.Application;
using IdentityManagement.Persistence;
using IdentityManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using APICoreBase.Extensions;
using APICoreBase.Middlewares;


var builder = WebApplication.CreateBuilder(args);

//Add DbContext with SQL Server connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
// Add services to the container.
builder.Services.AddSwaggerGen();

//Application, Infra and Persitence services config
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfraServices();

builder.Services.AddControllers();

builder.AddLogger();

// Add OpenTelemetry Tracing
//builder.Services.AddOpenTelemetry()
//    .WithTracing(tracerProviderBuilder =>
//    {
//        tracerProviderBuilder
//            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("IdentityManagementApi"))
//            .AddAspNetCoreInstrumentation()
//            .AddConsoleExporter(); // Exports traces to the console
//    });


var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextIntializer>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedDataAsync();
    }
}

// Add Middleware
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
