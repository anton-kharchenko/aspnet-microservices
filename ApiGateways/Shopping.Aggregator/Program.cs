using Shopping.Aggregator.Interfaces;
using Shopping.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ICatalogService, CatalogService>(
    c =>
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:CatalogUrl"]!)
);
builder.Services.AddHttpClient<IBasketService, BasketService>(
    c =>
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]!)
);
builder.Services.AddHttpClient<IOrderService, OrderService>(
    c =>
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:OrderingUrl"]!)
);

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
