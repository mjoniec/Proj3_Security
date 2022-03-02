using MiddlewareTest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseTestMiddleware();
app.MapWhen(
    _ => new Random().Next(2) == 0, 
    appBuilder => appBuilder.UseMiddleware<EvenNumberMiddleware>());//this will branch from pipeline so no hi from controller with even number
app.UseOddNumberMiddleware();
app.MapControllers();
app.Run();
