using Microsoft.EntityFrameworkCore;
using WSFastFood.Data;
using WSFastFood.Services.HashService;
using WSFastFood.Services.OrdersServices;
using WSFastFood.Services.ProductsServices;
using WSFastFood.Services.UsersServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHashService, Hash256Service>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<FastFoodContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

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
