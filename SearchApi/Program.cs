// services
using SearchApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGoogleSearchService, GoogleSearchService>();

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

// ?? what is a container and sth on ASP build in IServiceCollection / IServiceProvider 
 * ?? how to select kestrel ? from cmd/ azure deployed ...
 * ?? how does MapControllers() know what folder and classes to map to?
 * ?? what app.UseAuthorization(); is needed for ?
 * ?? () => {}
 * ?? .WithName()
 * ?? record

 * roznica dodawanie swagger screen
 * roznica dodawanie https
 * roznica dodawanie minimal controllers vs standardowo
 * nie ma startup 
 * nie ma definicji using ani klasy w projekt.cs - to jest funkcja c# czy szablonu asp ... co dokladnie daje core 6.0?
  
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