using LeaderBoard.Data;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
       ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"))
);

builder.Services.AddScoped<ILeaderBoardRepo, LeaderBoardRepo>();

// Add MVC services
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Use API controllers
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();