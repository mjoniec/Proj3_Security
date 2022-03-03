var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHsts();//adds hsts response header, thet client is supposed to obey, must go with forced https redirection
app.UseHttpsRedirection();//forces https redirection on each http request

app.MapControllers();
app.Run();
