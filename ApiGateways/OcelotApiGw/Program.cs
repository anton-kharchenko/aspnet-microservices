using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddOcelot().AddCacheManager(settings=>settings.WithDictionaryHandle());

builder.Host.ConfigureAppConfiguration(
    (hostContext, config) => {
        config.AddJsonFile($"ocelot.{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
    }
);

var app = builder.Build();

if (builder.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!").ConfigureAwait(false);
    });
});

await app.UseOcelot().ConfigureAwait(false);

app.Run();
