using MiddlewareTest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseTestMiddleware();
app.MapControllers();
app.Run();
