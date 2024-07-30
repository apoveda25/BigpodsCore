using Bigpods.Monolith.Configuration.Services;
using DotNetEnv;
using DotNetEnv.Configuration;

var builder = WebApplication.CreateBuilder(args: args);

builder.Configuration.AddDotNetEnv(".env", LoadOptions.TraversePath());

builder
    .Services.AddCors()
    // .AddOpenTelemetryConfiguration()
    // .AddLoggingConfiguration(builder: builder.Logging)
    .AddAuthenticationConfiguration()
    .AddAuthorizationConfiguration()
    .AddPersistenceConfiguration()
    .AddMappersConfiguration()
    .AddMediatorConfiguration()
    .AddGraphQLConfiguration()
    .AddModulesConfiguration();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// await app.ConfigureMigrations();

app.MapGraphQL();

app.UseCors(configurePolicy: options =>
    options.AllowAnyOrigin().WithMethods("GET", "POST").AllowAnyHeader()
);

await app.RunAsync();
