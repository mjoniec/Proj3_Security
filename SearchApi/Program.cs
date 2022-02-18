// services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// pipeline
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
app.MapControllers();
app.Run();

/*
 * ?? struktura launchsettings.json kazda linijka co robi, czym jest profiles, 
 * czemu jest 2 raz iis na poziomie obok profiles i w srodku profiles... 
 * iis nie ma na linuxie trzeba dac w tedy jakiegos odpowiednika iis
 
 * ?? how to select kestrel ? from cmd/ azure deployed ...
 * ?? how does MapControllers() know what folder and classes to map to?
 * ?? what app.UseAuthorization(); is needed for ?
 * ?? () => {}
 * ?? .WithName()
 * ?? record
  
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/