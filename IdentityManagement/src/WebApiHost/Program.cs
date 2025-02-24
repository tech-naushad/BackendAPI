using IdentityManagement.Application;
using IdentityManagement.Persistence;
using IdentityManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using APICore.AppSettings;
using APICore.Extensions;
using APICore.Middlewares;
using MessageBrokers.Extensions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

//Add DbContext with SQL Server connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
// Add services to the container.
builder.Services.AddSwaggerGen();

 

//Application, Infra and Persitence services config
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfraServices();
builder.Services.AddIdentityCoreServices();
builder.Services.AddMesageBrokerServices(builder.Configuration);

builder.Services.AddControllers();

builder.AddLogger();

// Add services to the container
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = "Bearer";
//    options.DefaultChallengeScheme = "GitHub";
//})

//.AddOAuth("GitHub", options =>
//{
//    options.ClientId = builder.Configuration["GitHub:ClientId"];
//    options.ClientSecret = builder.Configuration["GitHub:ClientSecret"];
//    options.CallbackPath = new PathString("/signin-github");

//    // GitHub OAuth Endpoints
//    options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
//    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
//    options.UserInformationEndpoint = "https://api.github.com/user";

//    // Scopes
//    options.Scope.Add("user:email"); // You can add more scopes as needed

//    // Save tokens
//    options.SaveTokens = true;

//    // Optional: Handle GitHub user information
//    options.Events = new OAuthEvents
//    {
//        OnCreatingTicket = async context =>
//        {
//            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
//            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);

//            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
//            response.EnsureSuccessStatusCode();

//            var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
//            context.RunClaimActions(user.RootElement);
//        }
//    };
//});

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
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
