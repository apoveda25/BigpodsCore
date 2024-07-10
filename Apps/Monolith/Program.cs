using Bigpods.Monolith.Config.Services;

var builder = WebApplication.CreateBuilder(args: args);

builder
    .Services.AddCors()
    // .AddOpenTelemetryConfiguration()
    // .AddLoggingConfiguration(builder: builder.Logging)
    .AddAuthenticationConfiguration(configuration: builder.Configuration)
    .AddAuthorizationConfiguration(configuration: builder.Configuration)
    .AddPersistenceConfiguration(configuration: builder.Configuration)
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
    options.AllowAnyOrigin().WithMethods(methods: ["POST"]).AllowAnyHeader()
);

app.Run();
