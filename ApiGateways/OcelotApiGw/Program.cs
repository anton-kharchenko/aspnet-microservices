using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddOcelot();
builder.Host.ConfigureAppConfiguration(
    (hostContext, config) => { config.AddJsonFile($"ocelot.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true); }
);

var app = builder.Build();
await app.UseOcelot().ConfigureAwait(false);
app.Run();
