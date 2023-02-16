using Ordering.API.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Data.Context;
using Ordering.Infrastructure.Data.Mocks;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddMapper();
builder.Services.AddConsumers();

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

app.MigrateDatabase<OrderContext>(
    (context, _) => {
        OrderContextMock.MockDataAsync(context).Wait();
    }
);
app.Run();
